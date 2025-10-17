using Application.Interfaces.Repositories;
using Application.Interfaces.Usecases;
using Domain.Models.Entities.Player;
using Domain.Models.Entities.Player.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.PlayerPresentation.UI
{ 
    [RequireComponent(typeof(Slider))]
    public class StaminaSliderView : MonoBehaviour
    {
        // TODO what if we load scriptable object (PlayerState) which loads player state
        private Player _model;
        private IPlayerUsecase _playerUsecase;
        [SerializeField] private Slider _slider;

        [Inject]
        private void Construct(IPlayerUsecase playerUsecase, IPlayerRepository repository)
        {
            _playerUsecase = playerUsecase;
            _model = repository.Get();
        }
        
        void OnEnable()
        {
            _model.OnPlayerStaminaChanged += OnStaminaChanged;
            UpdateSlider(_model.GetState());
        }

        void OnDisable()
        {
            _model.OnPlayerStaminaChanged -= OnStaminaChanged;
        }
        
        private void OnValidate()
        {
            if (_slider == null) {
                _slider = GetComponent<Slider>();
            }
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