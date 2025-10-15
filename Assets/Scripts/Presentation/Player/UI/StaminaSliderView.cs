using System.Runtime.InteropServices;
using Application.EventSystem;
using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Player.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.Player.UI
{ 
    [RequireComponent(typeof(Slider))]
    public class StaminaSliderView : MonoBehaviour
    {
        [Inject] IEventBus _eventBus;
        private IPlayerUsecase _playerUsecase;
        [SerializeField] private Slider _slider;

        [Inject]
        private void Construct(IEventBus eventBus, IPlayerUsecase playerUsecase)
        {
            _eventBus = eventBus;
            _playerUsecase = playerUsecase;
        }
        
        void OnEnable()
        {
            _eventBus.Subscribe<PlayerStaminaChangedEvent>(OnStaminaChanged);
            UpdateSlider(_playerUsecase.GetState());
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<PlayerStaminaChangedEvent>(OnStaminaChanged);
        }
        
        private void OnValidate()
        {
            if (_slider == null) {
                _slider = GetComponent<Slider>();
            }
        }

        private void OnPlayerCreated(PlayerCreatedEvent e)
        {
            UpdateSlider(e.PlayerState);
        }
        private void OnStaminaChanged(PlayerStaminaChangedEvent e)
        {
            UpdateSlider(e.PlayerState);
        }
        private void UpdateSlider(PlayerState state)
        {
            _slider.value = state.Stamina / state.MaxStamina;
        }
    }
}