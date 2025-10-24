using System.Numerics;
using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IPlayerSleepUsecase
    {
        bool TrySleep(Player player);
        bool IsPossibleToSleep(Player player);
        void SetPlayerBed(Player player, Vector3 bedSpawn);
        void StartPassOut(Player player);
        void EndPassOut(Player player);
    }
}