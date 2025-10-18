using System;
using System.Numerics;
using Domain.Models.Common;
using Domain.Models.Entities.BedModel;
using Domain.Models.Entities.Player.Events;
    
namespace Domain.Models.Entities.Player
{
    public class Player : BaseModel, IDamageable, IStaminaConsumer
    {
        private int _health;
        private float _stamina;
        private float _maxStamina;
        private Vector3 _position;
        // TODO Store Bed reference? Or store bed id? 
        private Vector3 _bedSpawn;

        public float Stamina => _stamina;
        public float MaxStamina => _maxStamina;
        
        public event Action<PlayerCreatedEvent> OnPlayerCreated;
        public event Action<PlayerTakeDamageEvent> OnPlayerTakeDamage;
        public event Action<PlayerNotEnoughStaminaEvent> OnPlayerNotEnoughStamina;
        public event Action<PlayerStaminaChangedEvent> OnPlayerStaminaChanged;
        public event Action<PlayerPassOutEvent> OnPassOut;
        
        public event Action<PlayerSetPositionEvent> OnPlayerSetPosition;
        
        public Player(PlayerDto dto)
        {
            _health = dto.Health;
            _stamina = dto.Stamina;
            _maxStamina = dto.MaxStamina;

            var createdEvent = new PlayerCreatedEvent(this, GetDto());
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

            var damageEvent = new PlayerTakeDamageEvent(null, GetDto());
            OnPlayerTakeDamage?.Invoke(damageEvent);
        }
        
        // TODO Move to constructor? (Bed is required for player)
        public void SetBed(Bed bed)
        {
            _bedSpawn = bed.GetDto().Position;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            OnPlayerSetPosition?.Invoke(new PlayerSetPositionEvent(this, GetDto()));
        }
        public void StartPassOut()
        {
            var e = new PlayerPassOutEvent(this, GetDto(), false);
            OnPassOut?.Invoke(e);
        }

        public void EndPassOut()
        {
            var e = new PlayerPassOutEvent(this, GetDto(), true);
            OnPassOut?.Invoke(e);
        }

        public bool TryConsumeStamina(object sender, float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }

            if (!HasStamina(amount))
            {
                var notEnoughEvent = new PlayerNotEnoughStaminaEvent(sender, GetDto());
                OnPlayerNotEnoughStamina?.Invoke(notEnoughEvent);
                return false;
            }

            _stamina -= amount;
            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetDto());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
            return true;
        }

        public void RestoreStamina(object sender, float amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("Amount must be greater than zero");
            }

            _stamina = Math.Min(_stamina + amount, _maxStamina);
            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetDto());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
        }

        public void SetStamina(object sender, float amount)
        {
            if (amount < 0) {
                throw new ArgumentException("Amount must be greater or equal than zero");
            }
            _stamina = Math.Min(amount, _maxStamina);

            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetDto());
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

        public Vector3 GetBedSpawn()
        {
            return _bedSpawn;
        }
        public PlayerDto GetDto()
        {
            return new PlayerDto
            {
                Health = _health,
                Stamina = _stamina,
                MaxStamina = _maxStamina,
                Position = _position,
            };
        }
    }
}