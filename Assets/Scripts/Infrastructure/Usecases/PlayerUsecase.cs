using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerUsecase : IPlayerUsecase
    {
        private IPlayerRepository _playerRepository;
        private IPlayerFactory _playerFactory;

        [Inject]
        public PlayerUsecase(IPlayerRepository repository, IPlayerFactory playerFactory)
        {
            _playerRepository = repository;
            _playerFactory = playerFactory;
            CreatePlayer();
        }
            
        public void TakeDamage(int amount)
        {
            _playerRepository.Get().TakeDamage(amount);
        }

        private void CreatePlayer()
        {
            var player = _playerFactory.CreateDefault();
            _playerRepository.Save(player);
        }
    }
}