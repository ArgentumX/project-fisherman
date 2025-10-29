using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public class StaminaMutation : Mutation
    {
        private readonly float _staminaBonus;

        public StaminaMutation(float staminaBonus) {
            _staminaBonus = staminaBonus;
        }
        

        public override void Apply(Player player)
        {
            player.UpdateStaminaMax(this, _staminaBonus);
        }

        public override void Revert(Player player)
        {
            player.UpdateStaminaMax(this, -1 * _staminaBonus);
        }
    }
}