using System;
using Application.Interfaces;
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

        protected IGameContext _context;
        protected Quest(string title, IGameContext context) {
            Title = title;
            _context = context;
        }

        public virtual void StartQuest()
        {
            if (Status != QuestStatus.NotStarted) 
                throw new InvalidOperationException("Cannot start a quest with a not started status");
            Status = QuestStatus.Active;
            var questEvent = new QuestStartedEvent(this, this);
            OnQuestStarted?.Invoke(questEvent);
            InitializeOnQuestStart(_context);
            RaiseUpdated();
        }
        protected abstract void InitializeOnQuestStart(IGameContext context);
        protected virtual void _FailQuest()
        {
            if (Status == QuestStatus.Failed || Status == QuestStatus.Completed) 
                throw new InvalidOperationException("Cannot fail a quest with a not Failed|Completed status");
            Status = QuestStatus.Failed;
            var questEvent = new QuestFailedEvent(this, this);
            OnQuestFailed?.Invoke(questEvent);
            RaiseUpdated();
        }

        protected virtual void _CompleteQuest()
        {
            if (Status == QuestStatus.Failed || Status == QuestStatus.Completed) 
                throw new InvalidOperationException("Cannot complete a quest with a not Failed|Completed status");
            Status = QuestStatus.Completed;
            var questEvent = new QuestCompletedEvent(this, this);
            OnQuestCompleted?.Invoke(questEvent);
            RaiseUpdated();
        }

        protected void RaiseUpdated()
        {
            var updatedEvent = new QuestUpdatedEvent(this, this);
            OnQuestUpdated?.Invoke(updatedEvent);
        }
    }
}