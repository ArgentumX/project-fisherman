using Application.EventSystem;
using Domain.Models.Entities.DayCycle;
using Domain.Models.Entities.DayCycle.Events;
using UnityEngine;
using Zenject;

namespace Presentation.DayCycle
{
    [RequireComponent(typeof(Light))]
    public class DayCycleView : MonoBehaviour
    {
        private IEventBus _eventBus;
        [SerializeField] private Light _sunLight; 
        [SerializeField] private Gradient _sunGradient;
        [SerializeField] private Gradient _ambientGradient;
        [SerializeField, Range(0, 1)] private float _timeProgress;
        private Vector3 _defaultLightAngles;
        
        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<DayCycleChangedEvent>(OnDayCycleChanged);
        }
        private void OnDisable()
        {
            _eventBus.Unsubscribe<DayCycleChangedEvent>(OnDayCycleChanged);
        }
        private void Start() => _defaultLightAngles = _sunLight.transform.localEulerAngles;
        private void OnDayCycleChanged(DayCycleChangedEvent e)
        {
            UpdateView(e.DayCycleState);
        }

        private void UpdateView(DayCycleState state)
        {
            var timeProgress = state.Time / state.DayLength;
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