using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Models.Entities.Quest.Events;

namespace Domain.Models.Entities.Quest
{
    public class ParallelCompositeQuest : CompositeQuest
    {
        private int _completedCount = 0;

        public ParallelCompositeQuest(string title, IGameContext context, IEnumerable<Quest> subQuests)
            : base(title, context,  subQuests) {
        }

        public override void StartQuest()
        {
            base.StartQuest();
            foreach (var quest in _subQuests.Where(q => q.Status == QuestStatus.NotStarted)) {
                quest.StartQuest();
            }
        }

        public override float Progress => _subQuests.Any() ? _completedCount / (float)_subQuests.Count : 1f;
        protected override void InitializeOnQuestStart(IGameContext context) {
        }

        protected override void HandleSubQuestCompletedInternal(QuestCompletedEvent e)
        {
            base.HandleSubQuestCompletedInternal(e);
            _completedCount++;
            if (_completedCount == _subQuests.Count) {
                _CompleteQuest();
            }
        }

        protected override void HandleSubQuestFailedInternal(QuestFailedEvent e)
        {
            base.HandleSubQuestFailedInternal(e);
            _FailQuest();
        }

        private void ValidateSubQuests() {
            if (_subQuests.Any(q => q.Status == QuestStatus.Failed)) {
                throw new Exception("Detected corrupted quest");
            }

            if (GetCompletedCount() == _subQuests.Count) {
                throw new Exception("Detected corrupted quest");
            }
        }
        private int GetCompletedCount()
        {
            return _subQuests.Count(q => q.Status == QuestStatus.Completed);
        }
    }
}