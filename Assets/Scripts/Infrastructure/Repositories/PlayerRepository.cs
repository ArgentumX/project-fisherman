using Application.EventSystem;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : Repository, IPlayerRepository
    {
        private Player _player;
        
        [Inject]
        public PlayerRepository(IEventBus eventBus) : base(eventBus) {
        }

        public Player Load()
        {
            _player = new Player(new PlayerState { Health = 100, Stamina = 100, MaxStamina = 100});
            SubscribeOnModel(_player);
            return _player;
        }

        public void Save(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}