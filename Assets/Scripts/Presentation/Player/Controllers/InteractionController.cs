using System;
using System.Collections;
using Application.Interfaces.Repositories;
using Domain.Models.Entities.Player;
using Presentation.Common;
using Presentation.PlayerPresentation.UI.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

// Required for new Input System

namespace Presentation.PlayerPresentation.Controllers
{
    public class InteractionController : MonoBehaviour, IInteractor<Player>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField, Min(0f)] private float interactableDistance = 2f;
        [SerializeField] private InteractionDisplay interactionDisplay;
        
        private Player _model;
        private IInteractable _currentTarget;
        private float _hoverCheckInterval = 0.1f; // Check every 0.1 seconds
        private float _lastHoverCheckTime;
        
        private InputAction _interactAction;
        private Coroutine _holdCoroutine;
        private bool _isHolding = false;
        
        private void Awake()
        {
            _interactAction = inputActions.FindActionMap("Player").FindAction("Interact");
        }

        [Inject]
        private void Construct(IPlayerRepository repository)
        {
            _model = repository.GetInstance();
        }

        private void OnEnable()
        {
            _interactAction.Enable();
            _interactAction.started += OnInteractStarted;
            _interactAction.canceled += OnInteractCanceled;
        }

        private void OnDisable()
        {
            _interactAction.Disable();
            _interactAction.started -= OnInteractStarted;
            _interactAction.canceled -= OnInteractCanceled;
            CancelHold();
        }
        private void Start()
        {
            mainCamera = Camera.main;
        }

        public Player GetModel() => _model;
        private void Update()
        {
            if (Time.time - _lastHoverCheckTime >= _hoverCheckInterval)
            {
                HandleHover();
                _lastHoverCheckTime = Time.time;
            }
        }
        
        private void HandleHover()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, interactableDistance, interactableLayer.value))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (interactable != _currentTarget)
                    {
                        if (_currentTarget != null) {
                            _currentTarget.OnHoverExit(this);
                        }
                        _currentTarget = interactable;
                        UpdateInteractionText();
                        _currentTarget.OnHoverEnter(this);
                    }
                    else {
                        _currentTarget.OnHoverStay(this);
                    }
                    return; 
                }
            }
            
            if (_currentTarget != null)
            {
                _currentTarget.OnHoverExit(this);
                CancelHold();
                _currentTarget = null;
                UpdateInteractionText();
            }
        }
        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            if (_currentTarget == null) return;
            if (!_currentTarget.CanInteract(this)) return;
            
            if (_currentTarget.GetInteractionTime() == 0)
            {
                _currentTarget.Interact(this);
                return;
            }

            _isHolding = true;
            _holdCoroutine = StartCoroutine(HoldInteractionRoutine());
        }
        private void OnInteractCanceled(InputAction.CallbackContext context)
        {
            CancelHold();
        }
        private void CancelHold()
        {
            if (_holdCoroutine != null)
            {
                StopCoroutine(_holdCoroutine);
                _holdCoroutine = null;
            }
            _isHolding = false;
            interactionDisplay.DisableHoldBar();
            UpdateInteractionText();
        }
        
        private IEnumerator HoldInteractionRoutine()
        {
            float elapsed = 0f;
            interactionDisplay.EnableHoldBar();
            interactionDisplay.ShowHoldProgress(0f);
            var holdDuration = _currentTarget.GetInteractionTime();
            while (_currentTarget != null && elapsed < holdDuration && _isHolding)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / holdDuration);
                interactionDisplay.ShowHoldProgress(progress);
                yield return null;
            }

            if (_isHolding && _currentTarget != null) {
                _currentTarget.Interact(this);
            }

            interactionDisplay.DisableHoldBar();
            _holdCoroutine = null;
            _isHolding = false;
            UpdateInteractionText();
        }

        private void UpdateInteractionText() {
            if (_currentTarget == null) {
                interactionDisplay.DisableInteractionText();
                return;
            }
            var message = _currentTarget.CanInteract(this) ? $"[E] - " + _currentTarget.GetDescription() : "[Невозможно]";
            interactionDisplay.ShowInteractionText(message);
        }


        private void OnDrawGizmos()
        {
            if (mainCamera != null)
            {
                Gizmos.color = Color.mediumVioletRed;
                Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * interactableDistance);
            }
        }
    }
}