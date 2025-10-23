using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Domain.Models.Entities.Quest.Events;
using ModestTree;

namespace Domain.Models.Entities.Quest
{
    public abstract class CompositeQuest : Quest
    {
        public IReadOnlyList<Quest> SubQuests => _subQuests.AsReadOnly();
        protected readonly List<Quest> _subQuests;

        protected CompositeQuest(string title, IEnumerable<Quest> subQuests, QuestStatus status = QuestStatus.NotStarted)
            : base(title, status)
        {
            // TODO why zenject errors are not displayed in editor?
            _subQuests = subQuests == null ? new List<Quest>() : subQuests.ToList();
            if (_subQuests.Count == 0) {
                throw new ArgumentException("SubQuests cannot be null or empty");
            }
            SubscribeToSubQuests();
        }
        public override float Progress
        {
            get
            {
                if (!_subQuests.Any()) return 1f;
                return _subQuests.Average(q => q.Progress);
            }
        }

        protected override void _CompleteQuest()
        {
            base._CompleteQuest();
            UnsubscribeFromSubQuests();
        }

        protected override void _FailQuest()
        {
            base._FailQuest();
            UnsubscribeFromSubQuests();
        }

        protected virtual void HandleSubQuestStarted(QuestStartedEvent e)
        {
            if (Status != QuestStatus.Active) 
                throw new WarningException("Detected link leak");
            RaiseUpdated(); 
        }

        /* Why no RaiseUpdate for these methods?
         Because their implementation decides Fail or Complete or ... Quest,
          so submethods will raise events anyway.*/ 
        protected abstract void HandleSubQuestCompletedInternal(QuestCompletedEvent e);

        protected abstract void HandleSubQuestFailedInternal(QuestFailedEvent e);

        protected virtual void HandleSubQuestUpdated(QuestUpdatedEvent e) {
            RaiseUpdated();
        }

        private void SubscribeToSubQuests()
        {
            if (_subQuests == null) 
                throw new ArgumentException("Detected empty composite quest");
            foreach (var quest in _subQuests)
            {
                quest.OnQuestStarted += HandleSubQuestStarted;
                quest.OnQuestCompleted += HandleSubQuestCompletedInternal;
                quest.OnQuestFailed += HandleSubQuestFailedInternal;
                quest.OnQuestUpdated += HandleSubQuestUpdated;
            }
        }
        private void UnsubscribeFromSubQuests()
        {
            if (_subQuests == null) 
                throw new ArgumentException("Detected empty composite quest");
            foreach (var quest in _subQuests)
            {
                quest.OnQuestStarted -= HandleSubQuestStarted;
                quest.OnQuestCompleted -= HandleSubQuestCompletedInternal;
                quest.OnQuestFailed -= HandleSubQuestFailedInternal;
                quest.OnQuestUpdated -= HandleSubQuestUpdated;
            }
        }
    }
}