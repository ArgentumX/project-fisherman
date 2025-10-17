using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private Player _player;

        [Inject]
        public PlayerRepository(IPlayerFactory playerFactory)
        {
            // TODO remove if file exists
            // TODO how to create player without view?
            var player = playerFactory.CreateDefault();
            Save(player);
        }

        public Player Get()
        {
            return _player;
        }

        public void Save(Player player)
        {
            _player = player;
        }
    }
}