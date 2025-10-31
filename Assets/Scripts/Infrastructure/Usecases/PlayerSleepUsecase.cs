using System;
using System.Numerics;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerSleepUsecase : IPlayerSleepUsecase
    {
        private IDayCycleRepository _dayCycleRepository;
        // TODO move to settings?
        private float _restorePercent = 0.5f;
        [Inject]
        public PlayerSleepUsecase(IDayCycleRepository dayCycleRepository)
        {
            _dayCycleRepository = dayCycleRepository;
        }
        public void StartSleep(Player player) {
            player.StartSleep(this);
        }

        public void EndSleep(Player player) {
            _dayCycleRepository.GetInstance().SetTimeOfDay(this, TimeOfDay.Morning);
            player.EndSleep(this);
        }

        public void SetPlayerBed(Player player, Vector3 bedSpawn)
        {
            player.SetBed(bedSpawn);
        }

        public bool IsPossibleToSleep(Player player)
        {
            var timeOfDay = _dayCycleRepository.GetInstance().TimeOfDay;
            if (timeOfDay is TimeOfDay.Evening or TimeOfDay.Night) {
                return true;
            }
            return false;
        }
        
        public void StartPassOut(Player player) {
            player.StartPassOut(this);
        }

        public void EndPassOut(Player player) {
            _dayCycleRepository.GetInstance().SetTimeOfDay(this, TimeOfDay.Morning);
            player.EndPassOut(this, _restorePercent);
        }
    }
}