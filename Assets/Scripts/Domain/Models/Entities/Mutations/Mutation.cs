using Domain.Models.Common;
using Domain.Models.Entities.Player;

namespace Domain.Models
{
    public abstract class Mutation : BaseModel
    {
        public abstract void Apply(Player player);
        public abstract void Revert(Player player);
        public abstract string GetDescription();
    }
}