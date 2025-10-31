using System.Numerics;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;

namespace Application.Interfaces.Usecases
{
    public interface IPlayerSleepUsecase
    {
        void StartSleep(Player player);
        void EndSleep(Player player);
        bool IsPossibleToSleep(Player player);
        void SetPlayerBed(Player player, Vector3 bedSpawn);
        void StartPassOut(Player player);
        void EndPassOut(Player player);
    }
}