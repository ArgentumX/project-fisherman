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

        [Inject]
        public PlayerUsecase(IPlayerRepository repository)
        {
            _playerRepository = repository;
        }
            
        public void TakeDamage(int amount)
        {
            _playerRepository.Get().TakeDamage(amount);
        }


    }
}