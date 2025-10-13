using Domain.Models.Entities.Player;

namespace Application.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        Player GetCurrentPlayer();
        void SavePlayer(Player player);
    }
}