using Application.Interfaces.Factories;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Zenject;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private Player _player;
        public PlayerRepository() { }

        public Player GetInstance()
        {
            return _player;
        }
        
        public void Save(Player player)
        {
            _player = player;
        }
    }
}