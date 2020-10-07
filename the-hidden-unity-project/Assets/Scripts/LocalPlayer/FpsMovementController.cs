using System;
using System.Linq;
using System.Numerics;
using Game;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace DefaultNamespace
{
    // FPS Controller
    // 1. Create a Parent Object like a 3D model
    // 2. Make the Camera the user is going to use as a child and move it to the height you wish. 
    // Escape Key: Escapes the mouse lock
    // Mouse click after pressing escape will lock the mouse again
    
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(FpsControllerState))]
    [DisallowMultipleComponent]
    public class FpsMovementController : NetworkBehaviour
    {
        public float movementSpeed = 2f;
        public float jumpHeight = 10f;
        
        public float gravityScale = 1.0f;

        public Vector3 friction = new Vector3(1, 0, 1);

        [HideInInspector] public Vector3 movementDirection = Vector3.zero;

        private InputMaster _inputMasterInstance;

        private InputMaster _inputMaster
        {
            get
            {
                if (_inputMasterInstance == null)
                    _inputMasterInstance = new InputMaster();
                return _inputMasterInstance;
            }
        }
        
        private CharacterController _characterController;
        private FpsControllerState _fpsControllerState;

        private Vector3 _move;
        private Vector3 _velocity;
        
        private Camera _camera;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            _characterController = GetComponent<CharacterController>();
            _fpsControllerState = GetComponent<FpsControllerState>();

            foreach (Camera cam in GetComponentsInChildren<Camera>())
                if (cam.CompareTag(Tags.MainCamera))
                    _camera = cam;
            
            _inputMaster.Enable();
            
            _inputMaster.FpsController.Movement.performed += (ctx) => OnMovementPerformed(ctx.ReadValue<Vector2>());
            _inputMaster.FpsController.Movement.canceled += (ctx) => OnMovementCancelled();
            _inputMaster.FpsController.Jump.performed += (ctx) => OnJump();
        }

        [Client]
        public void OnDisable()
        {
            if (isLocalPlayer)
                _inputMaster?.Disable();
        }

        [Client]
        private void Update()
        {
            if(isLocalPlayer)
            {
                if (Mathf.Abs(movementDirection.x) > 0 && Mathf.Abs(_velocity.x) < movementSpeed)
                {
                    _move += transform.right * (Mathf.Sign(movementDirection.x) * movementSpeed);
                }

                if (Mathf.Abs(movementDirection.z) > 0 && Mathf.Abs(_velocity.z) < movementSpeed)
                {
                    _move += transform.forward * (Mathf.Sign(movementDirection.z) * movementSpeed);
                }

                if (!_fpsControllerState.isClingingToWall)
                {
                    ApplyTranslationInputs();
                }
                else
                {
                    // Reset velocity so that once we stop clinging, the player doesn't maintain the previous velocity
                    _velocity = Vector3.zero;
                    _move = Vector3.zero;
                }
            }
            else
            {
                foreach (Camera cam in GetComponentsInChildren<Camera>())
                    cam.enabled = false;
                foreach (AudioListener audioListener in GetComponentsInChildren<AudioListener>())
                    audioListener.enabled = false;
            }
        }

        [Client]
        private void ApplyTranslationInputs()
        {
            // Multiply by delta time again to apply proper m/s^2 acceleration per frame rather than just 9.8/frame
            _velocity += Physics.gravity * (gravityScale * Time.deltaTime);

            CheckYVelocity();

            // Apply movement
            _characterController.Move(_move * Time.deltaTime);
            // Apply gravity/velocity
            _characterController.Move(_velocity * Time.deltaTime);

            // Apply friction
            if (_fpsControllerState.isGrounded)
            {
                _velocity = new Vector3(
                    Mathf.Lerp(_velocity.x, 0, friction.x * Time.deltaTime),
                    Mathf.Lerp(_velocity.y, 0, friction.y * Time.deltaTime),
                    Mathf.Lerp(_velocity.z, 0, friction.z * Time.deltaTime));
            }

            // Reset movement
            _move = Vector3.zero;
        }

        /**
         * Control Listeners
         */

        [Client]
        private void OnMovementPerformed(Vector2 direction)
        {
            movementDirection = new Vector3(direction.x, 0, direction.y);
        }

        [Client]
        private void OnMovementCancelled()
        {
            movementDirection = Vector3.zero;
        }

        [Client]
        private void OnJump()
        {
            if (_fpsControllerState.isGrounded)
            {
                JumpToHeight(jumpHeight);
            }
        }

        /**
         * END Control Listeners
         */

        [Client]
        public void JumpToHeight(float height)
        {
            CheckYVelocity();

            // vf^2 = vi^2 + 2ad
            // vi = sqrt(vf^2 - 2ad)
            // vi = sqrt(-2ad)
            // vi = sqrt(-2*g*jumpHeight)
            float vi = Mathf.Sqrt(-2 * Physics.gravity.y * gravityScale * height);
            _velocity += new Vector3(0, vi, 0);

            _fpsControllerState.isGrounded = false;
        }

        [Client]
        public void CheckYVelocity()
        {
            // Reset y velocity if is grounded
            if (_fpsControllerState.isGrounded && _velocity.y < 0)
                _velocity.y = 0;
        }

        public void AddVelocity(Vector3 velocity)
        {
            _velocity += velocity;
        }
    }
}