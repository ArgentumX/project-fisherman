using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IPlayerSleepUsecase
    {
        bool TrySleep(Player player);
        bool IsPossibleToSleep(Player player);
    }
}