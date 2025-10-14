using Application.EventSystem;
using Domain.Models.Entities.Player.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.Player
{ 
    [RequireComponent(typeof(Slider))]
    public class StaminaSliderView : MonoBehaviour
    {
        [Inject] IEventBus _eventBus;
        [SerializeField] private Slider _slider;
        
        void OnEnable()
        {
            _eventBus.Subscribe<PlayerStaminaChangedEvent>(UpdateSlider);
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<PlayerStaminaChangedEvent>(UpdateSlider);
        }

        private void OnValidate()
        {
            if (_slider == null) {
                _slider = GetComponent<Slider>();
            }
        }
        
        private void UpdateSlider(PlayerStaminaChangedEvent e)
        {
            _slider.value = e.PlayerState.Stamina / e.PlayerState.MaxStamina;
        }
    }
}