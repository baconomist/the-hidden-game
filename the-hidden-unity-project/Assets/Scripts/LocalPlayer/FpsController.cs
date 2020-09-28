using System;
using System.Linq;
using System.Numerics;
using Game;
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
    public class FpsController : MonoBehaviour
    {
        public float groundDetectionRadius = 1;
        public bool lockCursor = true;
        public Vector2 verticalRotBounds = new Vector2(40, 270);

        public float movementSpeed = 2f;
        public float jumpHeight = 10f;

        public bool enableForwardBoost = false;
        public float forwardBoostPower = 50f;

        public bool enableAttachToWalls = false;
        public float attachWallDistance = 5;

        public float gravityScale = 1.0f;

        public Vector3 friction = new Vector3(1, 0, 1);

        [HideInInspector] public Vector3 movementDirection = Vector3.zero;

        private InputMaster _inputMaster;
        private Camera _camera;
        private Vector2 _mouseDelta = Vector2.zero;
        private CharacterController _characterController;

        private bool _isGrounded = false;
        private bool _forwardBoostReady = false;

        private Vector3 _move;
        private Vector3 _velocity;

        private bool _boostKeyLetGoWhileOnWall = false;
        private bool _boostKeyHeldDown = false;
        private bool _isClingingToWall = false;

        private const float _wallAttachDebounceTime = 0.25f;
        private float _wallAttachDebounceTimer = 0;

        private int _wallAttachCountSinceGround = 0;

        private void OnEnable()
        {
            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
            }

            _inputMaster.Enable();
            
            _inputMaster.FpsController.Look.performed += (ctx) => OnLook(ctx.ReadValue<Vector2>());
            _inputMaster.FpsController.Movement.performed += (ctx) => OnMovementPerformed(ctx.ReadValue<Vector2>());
            _inputMaster.FpsController.Movement.canceled += (ctx) => OnMovementCancelled();
            _inputMaster.FpsController.Jump.performed += (ctx) => OnJump();

            _inputMaster.FpsController.BoostForward.started += (ctx) => OnBoostKeyDown();
            _inputMaster.FpsController.BoostForward.canceled += (ctx) => OnBoostKeyUp();
        }

        private void OnDisable()
        {
            _inputMaster?.Disable();
        }

        private void Awake()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        private void CheckState()
        {
            _isGrounded =
                Physics.CheckSphere(
                    transform.position - new Vector3(0,
                        _characterController.height / 2f - _characterController.center.y, 0),
                    groundDetectionRadius, LayerMask.GetMask(Layers.Ground), QueryTriggerInteraction.Ignore);

            if (_isGrounded)
                _wallAttachCountSinceGround = 0;
            
            
            // If on wall, we can boost, otherwise we wait to get grounded
            _forwardBoostReady = _isGrounded || _isClingingToWall;
        }

        private void Update()
        {
            CheckState();

            if (lockCursor)
                LockCursor();
            else
                UnlockCursor();

            if (Mathf.Abs(movementDirection.x) > 0 && Mathf.Abs(_velocity.x) < movementSpeed)
            {
                _move += transform.right * (Mathf.Sign(movementDirection.x) * movementSpeed);
            }

            if (Mathf.Abs(movementDirection.z) > 0 && Mathf.Abs(_velocity.z) < movementSpeed)
            {
                _move += transform.forward * (Mathf.Sign(movementDirection.z) * movementSpeed);
            }
            

            if (!_isClingingToWall)
            {
                // Try to cling to a wall
                // Can only attach to wall once per jump off ground and when holding shift key down
                // Also give a debounce delay to give the player a chance to detach from wall
                if (_wallAttachCountSinceGround < 1 && _boostKeyHeldDown && _wallAttachDebounceTimer > _wallAttachDebounceTime)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, attachWallDistance,
                        LayerMask.GetMask(Layers.Collideable));
                    if (colliders.Length > 0)
                    {
                        foreach (Collider col in colliders)
                        {
                            if (col.gameObject.CompareTag(Tags.Wall))
                            {
                                _isClingingToWall = true;
                                _wallAttachCountSinceGround++;
                                break;
                            }
                        }
                    }
                }
                
                ApplyTranslationInputs();
                _wallAttachDebounceTimer += Time.deltaTime;
            }
            else
            {
                // Reset velocity so that once we stop clinging, the player doesn't maintain the previous velocity
                _velocity = Vector3.zero;
                _move = Vector3.zero;
                _wallAttachDebounceTimer = 0;
            }
        }

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
            if (_isGrounded)
            {
                _velocity = new Vector3(
                    Mathf.Lerp(_velocity.x, 0, friction.x * Time.deltaTime),
                    Mathf.Lerp(_velocity.y, 0, friction.y * Time.deltaTime),
                    Mathf.Lerp(_velocity.z, 0, friction.z * Time.deltaTime));
            }

            // Reset movement
            _move = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (_mouseDelta.sqrMagnitude > 0.1f)
            {
                // Vertical Rotation
                // local euler angles 0-360 deg
                Vector3 camLocalEuler = _camera.transform.localEulerAngles;
                float appliedVerticalRot = -_mouseDelta.y * Time.deltaTime;
                float appliedHorizontalRot = _mouseDelta.x * Time.deltaTime;

                float verticalRot = camLocalEuler.x + appliedVerticalRot;
                if (verticalRot > verticalRotBounds.x && verticalRot < verticalRotBounds.y)
                {
                    if (Mathf.Abs(verticalRot - verticalRotBounds.x) < Mathf.Abs(verticalRot - verticalRotBounds.y))
                        verticalRot = verticalRotBounds.x;
                    else
                        verticalRot = verticalRotBounds.y;
                }

                _camera.transform.localEulerAngles = new Vector3(verticalRot, 0, 0);

                // Horizontal Rotation
                transform.Rotate(new Vector3(0, appliedHorizontalRot, 0));

                _mouseDelta = Vector2.zero;
            }
        }

        /**
         * Control Listeners
         */

        private void OnLook(Vector2 mouseDelta)
        {
            mouseDelta = new Vector2(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height);

            _mouseDelta = mouseDelta;
        }

        private void OnMovementPerformed(Vector2 direction)
        {
            movementDirection = new Vector3(direction.x, 0, direction.y);
        }

        private void OnMovementCancelled()
        {
            movementDirection = Vector3.zero;
        }

        private void OnBoostKeyDown()
        {
            _boostKeyHeldDown = true;
            BoostForward();
            
            if (_boostKeyLetGoWhileOnWall && _isClingingToWall)
            {
                _isClingingToWall = false;
                _boostKeyLetGoWhileOnWall = false;
            }
        }

        private void OnBoostKeyUp()
        {
            _boostKeyHeldDown = false;
            if (!_boostKeyLetGoWhileOnWall && _isClingingToWall)
            {
                _boostKeyLetGoWhileOnWall = true;
            }
        }

        private void OnJump()
        {
            if (_isGrounded)
            {
                JumpToHeight(jumpHeight);
            }
        }

        /**
         * END Control Listeners
         */

        private void BoostForward()
        {
            CheckYVelocity();

            if (enableForwardBoost && _forwardBoostReady)
            {
                _velocity += _camera.transform.forward * forwardBoostPower;
                // Jump up a little bit
                JumpToHeight(1);
            }
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void JumpToHeight(float height)
        {
            CheckYVelocity();

            // vf^2 = vi^2 + 2ad
            // vi = sqrt(vf^2 - 2ad)
            // vi = sqrt(-2ad)
            // vi = sqrt(-2*g*jumpHeight)
            float vi = Mathf.Sqrt(-2 * Physics.gravity.y * gravityScale * height);
            _velocity += new Vector3(0, vi, 0);

            _isGrounded = false;
        }

        private void CheckYVelocity()
        {
            // Reset y velocity if is grounded
            if (_isGrounded && _velocity.y < 0)
                _velocity.y = 0;
        }
    }
}