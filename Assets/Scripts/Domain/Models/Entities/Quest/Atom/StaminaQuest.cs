using Domain.Models.Common;
using Domain.Models.Entities.Player.Events;

namespace Domain.Models.Entities.Quest
{
    public class StaminaQuest : Quest, IEventHandler<PlayerStaminaChangedEvent>
    {
        public StaminaQuest(string title, QuestStatus status = QuestStatus.NotStarted) : base(title, status)
        {
        }

        public void Handle(PlayerStaminaChangedEvent e)
        {
            if (e.PlayerDto.Stamina == e.PlayerDto.MaxStamina) {
                _CompleteQuest();
            }
        }
    }
}