using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Models.Common;
using Domain.Models.Entities.Quest.Events;

namespace Domain.Models.Entities.Quest
{
    public class SequentialCompositeQuest : CompositeQuest
    {
        private int _currentIndex = 0;

        public SequentialCompositeQuest(string title, IGameContext context, IEnumerable<Quest> subQuests)
            : base(title, context, subQuests)
        {
        }

        public override void StartQuest()
        {
            base.StartQuest();
            _subQuests[_currentIndex].StartQuest();
        }
        

        public override float Progress => _subQuests.Any() ? _currentIndex / (float)_subQuests.Count : 1f;

        protected override void HandleSubQuestCompletedInternal(QuestCompletedEvent e)
        {
            if (Status != QuestStatus.Active)
                throw new Exception("Detected quest link leak");

            if (e.Quest != _subQuests[_currentIndex]) 
                throw new Exception("Detected corrupted quest");
            
            _currentIndex++;
            if (_currentIndex < _subQuests.Count) {
                _subQuests[_currentIndex].StartQuest();
            }
            else {
                _CompleteQuest();
            }
        }
        protected override void InitializeOnQuestStart(IGameContext context) {
        }

        protected override void HandleSubQuestFailedInternal(QuestFailedEvent e)
        {
            if (Status != QuestStatus.Active)
                throw new Exception("Detected quest link leak");
            _FailQuest();
        }

        private int GetFirstActiveIndex()
        {
            int result = 0;
            for (int i = 0; i < _subQuests.Count; i++)
            {
                var quest = _subQuests[i];
                if (quest.Status == QuestStatus.Completed) {
                    result = i + 1;
                } else if (quest.Status == QuestStatus.Active) {
                    break;
                }
                else {
                    throw new ArgumentException("Detected corrupted quest");
                }

            }
            if (result >= _subQuests.Count) 
                throw new ArgumentException("Detected corrupted quest");
            
            return result;
        }
    }
}