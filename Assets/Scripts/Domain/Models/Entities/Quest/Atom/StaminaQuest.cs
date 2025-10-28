using Application.Interfaces;
using Domain.Models.Common;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Player.Events;

namespace Domain.Models.Entities.Quest
{
    public class StaminaQuest : 
        Quest, 
        IEventHandler<PlayerStaminaChangedEvent>
    {
        public override float Progress { get => _progress; }
        private float _progress;
        public StaminaQuest(string title, IGameContext context) : base(title, context) {
        }
        
        public void Handle(PlayerStaminaChangedEvent e)
        {
            UpgradeProgressAndTryComplete(e.PlayerDto);
        }
        protected override void InitializeOnQuestStart(IGameContext context)
        {
            UpgradeProgressAndTryComplete(context.PlayerDto);
        }

        private void UpgradeProgressAndTryComplete(PlayerDto source)
        {
            UpdateProgress(source);
            if (!TryComplete())
                RaiseUpdated();
        }
        private void UpdateProgress(PlayerDto source) {
            _progress = source.Stamina / source.MaxStamina;
        }

        private bool TryComplete() {
            if (_progress == 1f) {
                _CompleteQuest();
                return true;
            }

            return false;
        }
        
    }
}