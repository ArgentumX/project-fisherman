using UnityEngine;
using UnityEngine.InputSystem;

namespace Presentation.Player.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [Header("Movement")]
        private Vector3 _currentMovement;
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _sprintMultiplier = 2.0f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _jumpHeight = 5.0f;

        [Header("Camera")]
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _mouseSensitivity = 0.3f;
        [SerializeField] private float _maxPitch = 80f;
        private float _pitch;

        [Header("Input Actions")]
        private CharacterController _characterController;
        [SerializeField] private InputActionAsset _inputActions;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private Vector2 _moveInput;
        private Vector2 _lookInput;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            var playerActionMap = _inputActions.FindActionMap("Player");
            _moveAction = playerActionMap.FindAction("Move");
            _lookAction = playerActionMap.FindAction("Look");
            _jumpAction = playerActionMap.FindAction("Jump");
            _sprintAction = playerActionMap.FindAction("Sprint");

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }
        void HandleMovement()
        {
            _moveInput = _moveAction.ReadValue<Vector2>();

            float speedMultiplier = _sprintAction.IsPressed() ? _sprintMultiplier : 1.0f;

            float verticalSpeed = _moveInput.y * _speed * speedMultiplier;
            float horizontalSpeed = _moveInput.x * _speed * speedMultiplier;

            Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
            horizontalMovement = transform.rotation * horizontalMovement;

            HandleGravityAndJumping();

            _currentMovement.x = horizontalMovement.x;
            _currentMovement.z = horizontalMovement.z;

            _characterController.Move(_currentMovement * Time.deltaTime);
        }

        void HandleGravityAndJumping()
        {
            if (_characterController.isGrounded)
            {
                _currentMovement.y = -0.5f;

                if (_jumpAction.triggered)
                {
                    _currentMovement.y = _jumpHeight;
                }
            }
            else
            {
                _currentMovement.y += _gravity * Time.deltaTime;
            }
        }

        void HandleRotation()
        {
            _lookInput = _lookAction.ReadValue<Vector2>();
            HandleHorizontalRotation();
            HandleVerticalRotation();
        }

        void HandleHorizontalRotation()
        {
            float yaw = _lookInput.x * _mouseSensitivity;
            transform.Rotate(0, yaw, 0);
        }

        void HandleVerticalRotation()
        {
            _pitch -= _lookInput.y * _mouseSensitivity;
            _pitch = Mathf.Clamp( _pitch, -_maxPitch, _maxPitch );
            _playerCamera.localRotation = Quaternion.Euler( _pitch, 0, 0 );
        }
    }
}