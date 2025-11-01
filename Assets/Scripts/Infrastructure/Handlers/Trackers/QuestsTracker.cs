using Application.Interfaces.Repositories;
using Domain.Models.Entities.Quest;
using Domain.Models.Entities.Quest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Handlers
{
    public class QuestsTracker
    {
        public event Action<QuestStartedEvent> OnQuestStarted;
        public event Action<QuestCompletedEvent> OnQuestCompleted;
        public event Action<QuestFailedEvent> OnQuestFailed;
        
        private readonly IQuestRepository _questRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly List<Quest> _activeQuests = new();
        private readonly Dictionary<Type, List<object>> _eventHandlers = new();

        [Inject]
        public QuestsTracker(IQuestRepository questRepository, IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
            _questRepository = questRepository;
            SubscribePlayerEvents(_playerRepository.GetInstance());
            LoadQuests();
        }

        private void SubscribePlayerEvents(Player player)
        {
            player.OnPlayerStaminaChanged += NotifyTrackedQuests;
        }
        
        // TODO unsubscribe, is it required at all?
        private void UnsubscribePlayerEvents(Player player) {
            player.OnPlayerStaminaChanged -= NotifyTrackedQuests;
        }
        
        private void LoadQuests()
        {
            foreach (Quest quest in _questRepository.GetAll())
            {
                SubscribeToQuestRecursive(quest);
            }
        }

        private void SubscribeToQuestRecursive(Quest quest)
        {
            SubscribeToQuest(quest);
            if (quest is CompositeQuest composite) {
                foreach (var subQuest in composite.SubQuests) {
                    SubscribeToQuestRecursive(subQuest);
                }
            }
        }

        private void UnsubscribeFromQuestRecursive(Quest quest)
        {
            UnsubscribeFromQuest(quest);
            if (quest is CompositeQuest composite) {
                foreach (var subQuest in composite.SubQuests) {
                    UnsubscribeFromQuestRecursive(subQuest);
                }
            }
        }
        private void SubscribeToQuest(Quest quest)
        {
            quest.OnQuestStarted += HandleQuestStarted;
            quest.OnQuestCompleted += HandleQuestCompleted;
            quest.OnQuestFailed += HandleQuestFailed;
        }
        private void UnsubscribeFromQuest(Quest quest)
        {
            quest.OnQuestStarted -= HandleQuestStarted;
            quest.OnQuestCompleted -= HandleQuestCompleted;
            quest.OnQuestFailed -= HandleQuestFailed;
        }
        
        private void HandleQuestStarted(QuestStartedEvent questEvent)
        {
            Quest quest = questEvent.Quest;
            RegisterListeners(quest);
            OnQuestStarted?.Invoke(questEvent);
        }

        private void HandleQuestCompleted(QuestCompletedEvent questEvent)
        {
            Quest quest = questEvent.Quest;
            UnregisterListeners(quest);
            OnQuestCompleted?.Invoke(questEvent);
        }

        private void HandleQuestFailed(QuestFailedEvent questEvent)
        {
            Quest quest = questEvent.Quest;
            UnregisterListeners(quest);
            OnQuestFailed?.Invoke(questEvent);
        }
        private void RegisterListeners(Quest quest)
        {
            if (_activeQuests.Contains(quest)) return;
            _activeQuests.Add(quest);
            
            var handlerInterfaces = quest.GetType()
                .GetInterfaces()
                .Where(i => i.IsGenericType && 
                            i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (var handlerInterface in handlerInterfaces)
            {
                var eventType = handlerInterface.GetGenericArguments()[0];
                if (!_eventHandlers.TryGetValue(eventType, out var list))
                {
                    list = new List<object>();
                    _eventHandlers[eventType] = list;
                }
                list.Add(quest);
            }
        }

        private void UnregisterListeners(Quest quest)
        {
            if (!_activeQuests.Contains(quest)) return;
            _activeQuests.Remove(quest);
            
            var handlerInterfaces = quest.GetType()
                .GetInterfaces()
                .Where(i => i.IsGenericType && 
                            i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (var handlerInterface in handlerInterfaces)
            {
                var eventType = handlerInterface.GetGenericArguments()[0];
                if (_eventHandlers.TryGetValue(eventType, out var list))
                {
                    list.Remove(quest);
                    if (list.Count == 0)
                    {
                        _eventHandlers.Remove(eventType);
                    }
                }
            }
        }
        
        private void NotifyTrackedQuests<TEvent>(TEvent e) where TEvent : Event
        {
            if (_eventHandlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                foreach (var handler in handlers.ToList())
                {
                    ((IEventHandler<TEvent>)handler).Handle(e);
                }
            }
        }
    }
}