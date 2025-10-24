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
        [SerializeField] private Light sunLight; 
        [SerializeField] private Gradient sunGradient;
        [SerializeField] private Gradient ambientGradient;
        [SerializeField, Range(0, 1)] private float displayTimeProgress;
        private Vector3 _defaultLightAngles;
        
        // TODO create Initializers system
        [Inject]
        private void Construct(IDayCycleRepository dayCycleRepository)
        {
            _model = dayCycleRepository.GetInstance();
        }


        private void OnEnable()
        {
            _model.OnDayCycleChanged += OnDayCycleChanged;
        }
        private void OnDisable()
        {
            _model.OnDayCycleChanged -= OnDayCycleChanged;
        }
        private void Start() => _defaultLightAngles = sunLight.transform.localEulerAngles;
        private void OnDayCycleChanged(DayCycleChangedEvent e)
        {
            UpdateView(e.DayCycleDto);
        }

        private void UpdateView(DayCycleDto dto)
        {
            var normalizedTime = dto.Time / dto.DayLength;
            UpdateLightView(normalizedTime);
        }

        private void UpdateLightView(float normalizedTime)
        {
            sunLight.color = sunGradient.Evaluate(normalizedTime);
            RenderSettings.ambientLight = ambientGradient.Evaluate(normalizedTime);
            sunLight.transform.localEulerAngles = new Vector3(360f * normalizedTime - 90, _defaultLightAngles.y, _defaultLightAngles.z);  
        }
        
        private void OnValidate()
        {
            sunLight ??= GetComponent<Light>();
            UpdateLightView(displayTimeProgress);
        }
    }
}