using System;
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
        [SerializeField] private InteractionText interactionText;
        
        private Player _model;
        private IInteractable _currentTarget;
        private float _hoverCheckInterval = 0.1f; // Check every 0.1 seconds
        private float _lastHoverCheckTime;
        
        private InputAction interactAction;
        
        private void Awake()
        {
            interactAction = inputActions.FindActionMap("Player").FindAction("Interact");
        }

        [Inject]
        public void Construct(IPlayerRepository repository)
        {
            _model = repository.Get();
        }

        private void OnEnable()
        {
            interactAction.Enable();
            interactAction.performed += OnInteractPerformed;
        }

        private void OnDisable()
        {
            interactAction.Disable();
            interactAction.performed -= OnInteractPerformed;
        }
        
        private void Start()
        {
            mainCamera = Camera.main;
        }

        public Player GetModel()
        {
            return _model;
        }
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
                        interactionText.Show($"[E] - " + interactable.GetDescription());
                        _currentTarget = interactable;
                        _currentTarget.OnHoverEnter(this);
                    }
                    else {
                        interactable.OnHoverStay(this);
                    }
                    return; 
                }
            }
            
            if (_currentTarget != null)
            {
                interactionText.Hide();
                _currentTarget.OnHoverExit(this);
                _currentTarget = null;
            }
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            if (_currentTarget != null)
            {
                _currentTarget.Interact(this);
            }
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