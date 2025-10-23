using Application.Interfaces.Repositories;
using Domain.Models.Entities.Quest;
using Domain.Models.Entities.Quest.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Player.Events;
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
            player.OnPlayerStaminaChanged += RaiseEventForQuests;
        }
        
        // TODO unsubscribe, is it required at all?
        private void UnsubscribePlayerEvents(Player player) {
            player.OnPlayerStaminaChanged -= RaiseEventForQuests;
        }
        
        private void LoadQuests()
        {
            foreach (Quest quest in _questRepository.GetAll())
            {
                // TODO Warning no unsubscribing, better to subscribe on QuestsEventProvider
                quest.OnQuestStarted += HandleQuestStarted;
                quest.OnQuestCompleted += HandleQuestCompleted;
                quest.OnQuestFailed += HandleQuestFailed;
                if (quest.Status == QuestStatus.Active) {
                    _activeQuests.Add(quest);
                    RegisterListeners(quest);
                }
            }
        }

        private void HandleQuestStarted(QuestStartedEvent questEvent)
        {
            Quest quest = _questRepository.Get(questEvent.QuestId);
            if (!_activeQuests.Contains(quest))
            {
                _activeQuests.Add(quest);
                RegisterListeners(quest);
            }
            OnQuestStarted?.Invoke(questEvent);
        }

        private void HandleQuestCompleted(QuestCompletedEvent questEvent)
        {
            Quest quest = _questRepository.Get(questEvent.QuestId);
            if (_activeQuests.Contains(quest))
            {
                UnregisterListeners(quest);
                _activeQuests.Remove(quest);
            }
            OnQuestCompleted?.Invoke(questEvent);
        }

        private void HandleQuestFailed(QuestFailedEvent questEvent)
        {
            Quest quest = _questRepository.Get(questEvent.QuestId);
            if (_activeQuests.Contains(quest))
            {
                UnregisterListeners(quest);
                _activeQuests.Remove(quest);
            }
            OnQuestFailed?.Invoke(questEvent);
        }

        private void RegisterListeners(Quest quest)
        {
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
                list.Add(quest); // quest implements IEventHandler<T>
            }
        }

        private void UnregisterListeners(Quest quest)
        {
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
        
        private void RaiseEventForQuests<TEvent>(TEvent e) where TEvent : BaseEvent
        {
            if (_eventHandlers.TryGetValue(typeof(TEvent), out var handlers))
            {
                foreach (var handler in handlers.ToList())
                {
                    ((IEventHandler<TEvent>)handler).Handle(e);
                }
            }
        }
        
        // TODO lets make QuestsTracker : IEventHandler<T_1>, ..., IEventHandler<T_N> and raise error if quest try register on unhandled events
    }
}