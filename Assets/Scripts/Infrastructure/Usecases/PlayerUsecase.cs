using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerUsecase : Usecase, IPlayerUsecase
    {
        private IPlayerRepository _playerRepository;
        private Player _player;

        [Inject]
        public PlayerUsecase(IPlayerRepository repository)
        {
            _playerRepository = repository;
        }
            
        public void TakeDamage(int amount)
        {
            _player.TakeDamage(amount);
        }
    }
}