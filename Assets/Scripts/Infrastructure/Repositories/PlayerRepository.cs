using Application.Interfaces.EventSystem;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : Repository, IPlayerRepository
    {
        private Player _player;
        
        [Inject]
        public PlayerRepository(IEventBus eventBus) : base(eventBus)
        {
            _player = new Player(new PlayerState() { Health = 100 });
            // Important - EventBus hook, requires unsubscribe (UnsubscribeModel) on destroy
            SubscribeModel(_player);
        }

        public Player GetCurrentPlayer() => _player;

        public void SavePlayer(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}