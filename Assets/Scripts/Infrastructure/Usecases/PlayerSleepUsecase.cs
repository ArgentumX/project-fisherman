using System;
using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Enums;
using Domain.Models.Entities.BedModel;
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
        
        public bool TrySleep(Player player)
        {
            if (!IsPossibleToSleep(player))
                return false;
            
            player.RestoreStamina(this, player.MaxStamina);
            _dayCycleRepository.Get().SetTimeOfDay(this, TimeOfDay.Morning);
            return true;
        }

        public void SetPlayerBed(Player player, Bed bed)
        {
            player.SetBed(bed);
        }

        public bool IsPossibleToSleep(Player player)
        {
            var timeOfDay = _dayCycleRepository.Get().TimeOfDay;
            if (timeOfDay is TimeOfDay.Evening or TimeOfDay.Night) {
                return true;
            }

            return false;
        }
        
        public void StartPassOut(Player player)
        {
            // TODO pass real bed
            player.StartPassOut();
        }

        public void EndPassOut(Player player)
        {
            // TODO replace some logic to player model
            player.SetPosition(player.GetBedSpawn());
            player.EndPassOut();
            player.SetStamina(this, Math.Min(player.MaxStamina, player.Stamina + player.MaxStamina * _restorePercent));
            _dayCycleRepository.Get().SetTimeOfDay(this, TimeOfDay.Morning);
        }
    }
}