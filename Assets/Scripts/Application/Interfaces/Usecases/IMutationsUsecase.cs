using Domain.Models;
using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IMutationsUsecase
    {
        public void AddRandomMutation(Player player);
        public void AddMutation(Player player, MutationType mutationType);
        public void RaiseMutatioStages(Player player);
    }
}