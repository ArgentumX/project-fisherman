using System;
using System.ComponentModel;
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

    public abstract class Quest : BaseModel
    {
        public event Action<QuestCompletedEvent> OnQuestCompleted;
        public event Action<QuestFailedEvent> OnQuestFailed;
        public event Action<QuestStartedEvent> OnQuestStarted;
        public event Action<QuestUpdatedEvent> OnQuestUpdated;

        public string Title { get; private set; }
        public QuestStatus Status { get; private set; }

        public virtual float Progress => Status == QuestStatus.Completed ? 1f : 0f;

        protected Quest(string title, QuestStatus status = QuestStatus.NotStarted)
        {
            Title = title;
            Status = status;
        }

        public virtual void StartQuest()
        {
            if (Status != QuestStatus.NotStarted) 
                throw new InvalidOperationException("Cannot start a quest with a not started status");
            Status = QuestStatus.Active;
            var questEvent = new QuestStartedEvent(this, Id);
            OnQuestStarted?.Invoke(questEvent);
            RaiseUpdated();
        }

        protected virtual void _FailQuest()
        {
            if (Status == QuestStatus.Failed || Status == QuestStatus.Completed) 
                throw new InvalidOperationException("Cannot fail a quest with a not Failed|Completed status");
            Status = QuestStatus.Failed;
            var questEvent = new QuestFailedEvent(this, Id);
            OnQuestFailed?.Invoke(questEvent);
            RaiseUpdated();
        }

        protected virtual void _CompleteQuest()
        {
            if (Status == QuestStatus.Failed || Status == QuestStatus.Completed) 
                throw new InvalidOperationException("Cannot complete a quest with a not Failed|Completed status");
            Status = QuestStatus.Completed;
            var questEvent = new QuestCompletedEvent(this, Id);
            OnQuestCompleted?.Invoke(questEvent);
            RaiseUpdated();
        }

        protected void RaiseUpdated()
        {
            var updatedEvent = new QuestUpdatedEvent(this, Id);
            OnQuestUpdated?.Invoke(updatedEvent);
        }
    }
}