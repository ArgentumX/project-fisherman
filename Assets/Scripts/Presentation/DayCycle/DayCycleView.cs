using Application.Interfaces.Repositories;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using UnityEngine;
using Zenject;

namespace Presentation.DayCyclePresentation
{
    [RequireComponent(typeof(Light))]
    public class DayCycleView : MonoBehaviour
    {
        private DayCycle _model;
        [SerializeField] private Light _sunLight; 
        [SerializeField] private Gradient _sunGradient;
        [SerializeField] private Gradient _ambientGradient;
        [SerializeField, Range(0, 1)] private float _timeProgress;
        private Vector3 _defaultLightAngles;
        
        // TODO create Initializers system
        [Inject]
        private void Construct(IDayCycleRepository dayCycleRepository)
        {
            _model = dayCycleRepository.Get();
        }

        // TODO все Construct private
        private void OnEnable()
        {
            _model.OnDayCycleChanged += OnDayCycleChanged;
        }
        private void OnDisable()
        {
            _model.OnDayCycleChanged -= OnDayCycleChanged;
        }
        private void Start() => _defaultLightAngles = _sunLight.transform.localEulerAngles;
        private void OnDayCycleChanged(DayCycleChangedEvent e)
        {
            UpdateView(e.DayCycleDto);
        }

        private void UpdateView(DayCycleDto dto)
        {
            var timeProgress = dto.Time / dto.DayLength;
            UpdateLightView(timeProgress);
        }

        private void UpdateLightView(float timeProgress)
        {
            _sunLight.color = _sunGradient.Evaluate(timeProgress);
            RenderSettings.ambientLight = _ambientGradient.Evaluate(timeProgress);
            _sunLight.transform.localEulerAngles = new Vector3(360f * timeProgress - 90, _defaultLightAngles.y, _defaultLightAngles.z);  
        }
        
        private void OnValidate()
        {
            if (_sunLight == null) {
                _sunLight = GetComponent<Light>();
            }
            UpdateLightView(_timeProgress);
        }
        

    }
}