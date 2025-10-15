using Application.EventSystem;
using Application.Interfaces.Factories;
using Domain.Models.Entities.Player;

namespace Infrastructure.Factories
{
    public class PlayerFactory : DomainEventsPublishFactory, IPlayerFactory
    {
        public PlayerFactory(IDomainEventsPublisher domainEventsPublisher) : base(domainEventsPublisher)
        {
        }

        public Player CreateDefault()
        {
            var state = new PlayerState()
            {
                Health = 100,
                MaxStamina = 100,
                Stamina = 100,
            };
            return Create(state);
        }

        public Player Create(PlayerState state)
        {
            var player = new Player(state);
            RegisterModel(player);
            return player;
        }
    }
}