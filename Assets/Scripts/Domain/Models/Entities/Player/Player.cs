using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using Domain.Models.Common;
using Domain.Models.Entities.Player.Events;
using Domain.Models.Entities.Quest;

namespace Domain.Models.Entities.Player
{
    public class Player : 
        BaseModel,
        IDamageable, 
        IStaminaConsumer
    {
        private int _health;
        
        private float _stamina;
        private float _baseMaxStamina;
        private float _maxStaminaBonus = 0;
        private Vector3 _position;
        private Vector3 _bedSpawn;
        private readonly List<Mutation> _mutations = new();
        public PlayerQuestsProgress QuestsProgress { get; } = new();
        public float Stamina => _stamina;
        public float BaseMaxStamina => _baseMaxStamina;
        public float MaxStamina => _baseMaxStamina + _maxStaminaBonus;
        public bool IsSleep { get; private set; }
        
        public event Action<PlayerTakeDamageEvent> OnPlayerTakeDamage;
        public event Action<PlayerNotEnoughStaminaEvent> OnPlayerNotEnoughStamina;
        public event Action<PlayerStaminaChangedEvent> OnPlayerStaminaChanged;
        public event Action<PlayerPassOutEvent> OnPassOut;
        public event Action<PlayerSetPositionEvent> OnPlayerSetPosition;
        
        public Player(PlayerDto dto)
        {
            _health = dto.Health;
            _baseMaxStamina = dto.BaseMaxStamina;
            _stamina = Math.Min(dto.Stamina, MaxStamina); 
            // TODO teleport to old position\
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
        
        public void SetBed(Vector3 spawnPosition) {
            _bedSpawn = spawnPosition;
        }
        
        public void StartSleep(object sender) {
            IsSleep = true;
        }

        public void EndSleep(object sender)
        {
            SetPosition(GetBedSpawn());
            RestoreStamina(this, MaxStamina);
            IsSleep = false;
        }

        public void AddMutation(Mutation mutation) {
            // TODO type filtering
            mutation.Apply(this);
            _mutations.Add(mutation);
        }

        public void RemoveMutation(Mutation mutation) {
            // TODO type filtering
            mutation.Revert(this);
            _mutations.Remove(mutation);
        }

        public void ApplyMutations() {
            foreach (var mutation in _mutations) {
                mutation.Apply(this);
            }
        }

        public IReadOnlyCollection<Mutation> GetMutations() {
            return _mutations.AsReadOnly();
        }
        public void UpdatePosition(Vector3 newPosition) {
            _position = newPosition;
        }
        public void SetPosition(Vector3 position)
        {
            _position = position;
            OnPlayerSetPosition?.Invoke(new PlayerSetPositionEvent(this, GetDto()));
        }
        public void StartPassOut(object sender)
        {
            IsSleep = true;
            var e = new PlayerPassOutEvent(this, GetDto(), false);
            OnPassOut?.Invoke(e);
        }

        public void EndPassOut(object sender, float restorePercent)
        {
            SetPosition(GetBedSpawn());
            SetStamina(this, Math.Min(MaxStamina, Stamina + MaxStamina * restorePercent));
            var e = new PlayerPassOutEvent(this, GetDto(), true);
            OnPassOut?.Invoke(e);
            IsSleep = false;
        }

        public bool TryConsumeStamina(object sender, float amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount must be greater or equal than zero");
            

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
        
        public void UpdateStaminaMax(object sender, float bonus) {
            if (bonus == 0) return;
            _maxStaminaBonus += bonus;
            if (MaxStamina < 0)
                _maxStaminaBonus = -1 * _baseMaxStamina;
                
            SetStamina(sender, _stamina);
        }
        
        public void RestoreStamina(object sender, float amount)
        {
            if (amount <= 0) 
                throw new ArgumentException("Amount must be greater than zero");
            
            _stamina = Math.Min(_stamina + amount, MaxStamina);
            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetDto());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
        }

        public void SetStamina(object sender, float amount)
        {
            if (amount < 0) 
                throw new ArgumentException("Amount must be greater or equal than zero");
            
            _stamina = Math.Min(amount, MaxStamina);

            var staminaChangedEvent = new PlayerStaminaChangedEvent(sender, GetDto());
            OnPlayerStaminaChanged?.Invoke(staminaChangedEvent);
        }

        public bool HasStamina(float amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");
            
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
                BaseMaxStamina = _baseMaxStamina,
                MaxStamina = MaxStamina,
                Position = _position,
            };
        }
    }
}