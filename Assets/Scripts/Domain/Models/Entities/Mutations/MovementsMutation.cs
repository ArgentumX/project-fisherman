using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public class MovementsMutation : Mutation
    {
        public MovementsMutation(MutationStage stage) : base(stage)
        {
        }
        public override void Apply(Player player) {
        }

        public override void Revert(Player player) {
        }

        public override string GetDescription() {
            return $"Movements mutation: {Stage}";
        }
    }
}