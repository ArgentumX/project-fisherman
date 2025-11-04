using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public class EyesMutation : Mutation
    {
        public EyesMutation(MutationStage stage) : base(stage)
        {
        }

        public override void Apply(Player player) {
        }

        public override void Revert(Player player) {
        }

        public override string GetDescription() {
            return $"Eyes mutation: {Stage}";
        }
    }
}