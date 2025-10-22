using System;
using Domain.Models.Common;
using Domain.Models.Entities.Quest.Events;

namespace Domain.Models.Entities.Quest
{
    public enum QuestStatus
    {
        NotStarted,
        Active,
        Completed,
        Failed
    }
    public class Quest : BaseModel
    {
        public event Action<QuestCompletedEvent> OnQuestCompleted;
        public event Action<QuestFailedEvent> OnQuestFailed;
        public event Action<QuestStartedEvent> OnQuestStarted;

        public string Title { get; private set; }
        public QuestStatus Status { get; private set; }
        
        public Quest(string title, QuestStatus status = QuestStatus.NotStarted) {
            Title = title;
            Status = status;
        }

        public void StartQuest()
        {
            Status = QuestStatus.Active;
            var questEvent = new QuestStartedEvent(this, Id);
            OnQuestStarted?.Invoke(questEvent);
        }

        public void TryUpdateStatus() { }
        
        protected void _FailQuest() {
            Status = QuestStatus.Failed;
            var questEvent = new QuestFailedEvent(this, Id);
            OnQuestFailed?.Invoke(questEvent);
        }

        protected void _CompleteQuest() {
            Status = QuestStatus.Completed;
            var questEvent = new QuestCompletedEvent(this, Id);
            OnQuestCompleted?.Invoke(questEvent);
        }
        
        
    }
}