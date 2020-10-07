using Game;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(FpsMovementController))]
    [RequireComponent(typeof(FpsControllerState))]
    [DisallowMultipleComponent]
    public class TheHiddenFpsController : NetworkBehaviour
    {
        public bool enableForwardBoost = false;
        public float forwardBoostPower = 50f;

        public bool enableAttachToWalls = false;
        public float attachWallDistance = 5;
        
        private bool _forwardBoostReady = false;
        
        private bool _boostKeyLetGoWhileOnWall = false;
        private bool _boostKeyHeldDown = false;

        private const float _wallAttachDebounceTime = 0.25f;
        private float _wallAttachDebounceTimer = 0;

        private int _wallAttachCountSinceGround = 0;
        
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

        private FpsMovementController _fpsMovementController;
        private FpsControllerState _fpsControllerState;
        private Camera _camera;
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            _fpsMovementController = GetComponent<FpsMovementController>();
            _fpsControllerState = GetComponent<FpsControllerState>();
            
            foreach (Camera cam in GetComponentsInChildren<Camera>())
                if (cam.CompareTag(Tags.MainCamera))
                    _camera = cam;
            
            _inputMaster.Enable();
            
            _inputMaster.FpsController.BoostForward.started += (ctx) => OnBoostKeyDown();
            _inputMaster.FpsController.BoostForward.canceled += (ctx) => OnBoostKeyUp();
        }

        private void CheckState()
        {
            // If on wall, we can boost, otherwise we wait to get grounded
            _forwardBoostReady = _fpsControllerState.isGrounded || _fpsControllerState.isClingingToWall;
            
            if (_fpsControllerState.isGrounded)
                _wallAttachCountSinceGround = 0;
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                CheckState();
                
                if (!_fpsControllerState.isClingingToWall)
                {
                    // Try to cling to a wall
                    // Can only attach to wall once per jump off ground and when holding shift key down
                    // Also give a debounce delay to give the player a chance to detach from wall
                    if (_wallAttachCountSinceGround < 1 && _boostKeyHeldDown &&
                        _wallAttachDebounceTimer > _wallAttachDebounceTime)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.position, attachWallDistance,
                            LayerMask.GetMask(Layers.Collideable));
                        if (colliders.Length > 0)
                        {
                            foreach (Collider col in colliders)
                            {
                                if (col.gameObject.CompareTag(Tags.Wall))
                                {
                                    _fpsControllerState.isClingingToWall = true;
                                    _wallAttachCountSinceGround++;
                                    break;
                                }
                            }
                        }
                    }
                    
                    _wallAttachDebounceTimer += Time.deltaTime;
                }
                else
                {
                    _wallAttachDebounceTimer = 0;
                }
            }
        }
        
        [Client]
        private void OnBoostKeyDown()
        {
            _boostKeyHeldDown = true;
            BoostForward();

            if (_boostKeyLetGoWhileOnWall && _fpsControllerState.isClingingToWall)
            {
                _fpsControllerState.isClingingToWall = false;
                _boostKeyLetGoWhileOnWall = false;
            }
        }

        [Client]
        private void OnBoostKeyUp()
        {
            _boostKeyHeldDown = false;
            if (!_boostKeyLetGoWhileOnWall && _fpsControllerState.isClingingToWall)
            {
                _boostKeyLetGoWhileOnWall = true;
            }
        }
        
        [Client]
        private void BoostForward()
        {
            _fpsMovementController.CheckYVelocity();

            if (enableForwardBoost && _forwardBoostReady)
            {
                _fpsMovementController.AddVelocity(_camera.transform.forward * forwardBoostPower);
                // Jump up a little bit
                _fpsMovementController.JumpToHeight(1);
            }
        }
    }
}