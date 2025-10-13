using Domain.Models.Common;
using Domain.Models.Entities.Player.Events;

namespace Domain.Models.Entities.Player
{
    public class Player : BaseModel, IDamageable
    {
        private int _health;
        
        public Player(PlayerState state)
        {
            _health = state.Health;
        }
        
        public void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health <= 0) {
                _health = 0;
                // TODO Death event
            }
            
            AddDomainEvent(new PlayerTakeDamageEvent(null, GetState()));
        }

        public PlayerState GetState()
        {
            return new PlayerState
            {
                Health = _health
            };
        }
    }
}