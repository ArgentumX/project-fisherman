using System;
using Domain.Models.Common;
using Domain.Models.Entities.Player.Events;

namespace Domain.Models.Entities.Player
{
    public class Player : BaseModel, IDamageable, IStaminaConsumer
    {
        private int _health;
        private float _stamina;
        private float _maxStamina;

        public float Stamina => _stamina;
        public float MaxStamina => _maxStamina;
        public Player(PlayerState state)
        {
            _health = state.Health;
            _stamina = state.Stamina;
            _maxStamina = state.MaxStamina;
            AddDomainEvent(new PlayerCreatedEvent(this, GetState()));
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

        public bool TryConsumeStamina(object sender,float amount)
        {
            if (amount < 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            if (!HasStamina(amount)) {
                AddDomainEvent(new PlayerNotEnoughtStaminaEvent(sender, GetState()));
                return false;
            }
            _stamina -= amount;
            AddDomainEvent(new PlayerStaminaChangedEvent(sender, GetState()));
            return true;
        }

        public void RestoreStamina(object sender, float amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            _stamina = Math.Min(_stamina + amount, _maxStamina);
            AddDomainEvent(new PlayerStaminaChangedEvent(sender, GetState()));
        }

        public void SetStamina(object sender,float amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            _stamina = Math.Max(amount, _maxStamina);
            AddDomainEvent(new PlayerStaminaChangedEvent(sender, GetState()));
        }

        public bool HasStamina(float amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            return _stamina >= amount;
        }
        
        public PlayerState GetState()
        {
            return new PlayerState
            {
                Health = _health,
                Stamina = _stamina,
                MaxStamina = _maxStamina,
            };
        }
    }
}