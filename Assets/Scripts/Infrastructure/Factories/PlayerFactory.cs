using Application.Interfaces.Factories;
using Domain.Models;
using Domain.Models.Entities.Player;

namespace Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private PlayerFactory() { }
        public Player CreateDefault()
        {
            var state = new PlayerDto()
            {
                Health = 100,
                BaseMaxStamina = 100,
                Stamina = 20,
            };
            return Create(state);
        }

        public Player Create(PlayerDto dto)
        {
            var player = new Player(dto);
            player.AddMutation(new StaminaMutation(150));
            return player;
        }
    }
}