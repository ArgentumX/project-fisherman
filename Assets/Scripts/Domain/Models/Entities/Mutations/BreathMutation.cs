using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public class BreathMutation : Mutation
    {
        public BreathMutation(MutationStage stage) : base(stage) {
        }

        public override void Apply(Player player) {
        }

        public override void Revert(Player player) {
        }

        public override string GetDescription() {
            return $"Breath mutation: {Stage}";
        }
        
    }
}