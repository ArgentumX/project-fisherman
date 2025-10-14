using Application.EventSystem;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player.Events;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace Presentation
{
    [RequireComponent(typeof(Slider))]
    public class ExampleHealthView : MonoBehaviour
    {
        [Inject] IPlayerUsecase _playerUsecase;
        [Inject] IEventBus _eventBus;
        [SerializeField] private Slider _slider;
        private void Awake()
        {
            // Нужно по умному подписывать и отписываться в IDisposable - имей это в виду
            _eventBus.Subscribe<PlayerTakeDamageEvent>(UpdateSlider);
        }

        private void OnValidate()
        {
            if (_slider == null) {
                _slider = GetComponent<Slider>();
            }
        }
        public void Update()
        {
            _playerUsecase.TakeDamage(1);
        }

        private void UpdateSlider(PlayerTakeDamageEvent e)
        {
            _slider.value = (float) e.PlayerState.Health / 100;
        }
        
    }
}