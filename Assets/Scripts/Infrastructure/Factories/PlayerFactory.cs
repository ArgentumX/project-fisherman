using Application.Interfaces.Factories;
using Domain.Models.Entities.Player;

namespace Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private PlayerFactory() { }
        public Player CreateDefault()
        {
            var state = new PlayerState()
            {
                Health = 100,
                MaxStamina = 100,
                Stamina = 50,
            };
            return Create(state);
        }

        public Player Create(PlayerState state)
        {
            var player = new Player(state);
            return player;
        }
    }
}