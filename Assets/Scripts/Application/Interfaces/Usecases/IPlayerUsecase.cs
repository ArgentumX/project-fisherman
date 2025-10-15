using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IPlayerUsecase
    {
        void TakeDamage(int amount);
        PlayerState GetState();
    }
}