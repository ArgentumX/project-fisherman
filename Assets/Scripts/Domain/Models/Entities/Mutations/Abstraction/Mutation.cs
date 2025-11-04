using Domain.Models.Common;
using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public abstract class Mutation : BaseModel
    {
        public MutationStage Stage => _stage;
        public abstract void Apply(Player player);
        public abstract void Revert(Player player);
        public abstract string GetDescription();
        private MutationStage _stage;

        protected Mutation(MutationStage stage) {
            _stage = stage;
        }
        public void RaiseStage()
        {
            if (_stage == MutationStage.Terminal)
                return;
            _stage++;
        }
    }
}