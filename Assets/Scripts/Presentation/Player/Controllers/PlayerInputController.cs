using System;
using Domain.Models.Entities.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Presentation.PlayerPresentation.Controllers
{
    [RequireComponent(typeof(InteractionController), typeof(MovementController))]
    public class PlayerInputController : MonoBehaviour
    {
        public InteractionController InteractionController => interactionController;
        public MovementController MovementController => movementController;
        public InputActionAsset InputActionsAsset => inputActions;
        
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private InteractionController interactionController;
        [SerializeField] private MovementController movementController;
        
        private void Reset() {
            interactionController = GetComponent<InteractionController>();
            movementController = GetComponent<MovementController>();
        }
    }
}