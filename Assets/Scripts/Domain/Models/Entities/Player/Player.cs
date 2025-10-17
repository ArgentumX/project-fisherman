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
        
        public event Action<PlayerCreatedEvent> OnPlayerCreated;
        public event Action<PlayerTakeDamageEvent> OnPlayerTakeDamage;
        public event Action<PlayerNotEnoughStaminaEvent> OnPlayerNotEnoughStamina;
        public event Action<PlayerStaminaChangedEvent> OnPlayerStaminaChanged;

        public Player(PlayerState state)
        {
            _health = state.Health;
            _stamina = state.Stamina;
            _maxStamina = state.MaxStamina;

            var createdEvent = new PlayerCreatedEvent(this, GetState());
            OnPlayerCreated?.Invoke(createdEvent);
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                _health = 0;
                // TODO Death event (пока не реализован)
            }

            var damageEvent = new PlayerTakeDamageEvent(null, GetState());
            OnPlayerTakeDamage?.Invoke(damageEvent);
        }

        public bool TryConsumeStamina(object sender, float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }

            if (!HasStamina(amount))
            {
                var notEnoughEvent = new PlayerNotEnoughStaminaEvent(sender, GetState());
                OnPlayerNotEnoughStamina?.Invoke(notEnoughEvent);
                return false;
            }

            _stamina -= amount;
            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetState());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
            return true;
        }

        public void RestoreStamina(object sender, float amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("Amount must be greater than zero");
            }

            _stamina = Math.Min(_stamina + amount, _maxStamina);
            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetState());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
        }

        public void SetStamina(object sender, float amount)
        {
            if (amount < 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            _stamina = Math.Min(amount, _maxStamina);

            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetState());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
        }

        public bool HasStamina(float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
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