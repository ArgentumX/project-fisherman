using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure
{
    public class GameContext : IGameContext
    {
        public PlayerDto PlayerDto => _playerRepository.GetInstance().GetDto();
        public DayCycleDto DayCycleDto => _dayCycleRepository.GetInstance().GetDto();

        private IPlayerRepository _playerRepository;
        private IDayCycleRepository _dayCycleRepository;
        
        [Inject]
        private GameContext(IPlayerRepository playerRepository, IDayCycleRepository dayCycleRepository)
        {
            _playerRepository = playerRepository;
            _dayCycleRepository = dayCycleRepository;
        }
        
    }
}