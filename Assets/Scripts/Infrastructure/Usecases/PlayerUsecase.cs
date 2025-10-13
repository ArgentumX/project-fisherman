using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Zenject;

namespace Infrastructure.Usecases
{
    public class PlayerUsecase : Usecase, IPlayerUsecase
    {
        private IPlayerRepository _playerRepository;

        [Inject]
        public PlayerUsecase(IPlayerRepository repository)
        {
            _playerRepository = repository;
        }
            
        public void TakeDamage(int amount)
        {
            _playerRepository.GetCurrentPlayer().TakeDamage(amount);
        }
    }
}