using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IPlayerUsecase
    {
        void TakeDamage(Player player, int amount);
    }
}