using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    public class BasicFpsAnimatorController : AnimatorController
    {
        private InputMaster _inputMaster;

        private NetworkModelManager _networkModelManager;

        [Client]
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            _networkModelManager = GetComponent<NetworkModelManager>();

            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
            }

            _inputMaster.Enable();

            _inputMaster.FpsController.Movement.performed += (ctx) => CmdAnimateWalkOnServer(true);
            _inputMaster.FpsController.Movement.canceled += (ctx) => CmdAnimateWalkOnServer(false);

            _inputMaster.FpsController.BoostForward.performed += (ctx) => CmdAnimateRunOnServer(true);
            _inputMaster.FpsController.BoostForward.canceled += (ctx) => CmdAnimateRunOnServer(false);

            _inputMaster.FpsController.Crouch.performed += (ctx) => CmdAnimateCrouchOnServer(true);
            _inputMaster.FpsController.Crouch.canceled += (ctx) => CmdAnimateCrouchOnServer(false);
            
            _inputMaster.FpsController.Reload.performed += (ctx) => CmdAnimateReloadOnServer(true);
            _inputMaster.FpsController.Reload.canceled += (ctx) => CmdAnimateReloadOnServer(false);
        }

        [Client]
        private void OnDisable()
        {
            if (hasAuthority)
                _inputMaster.Disable();
        }

        [Command]
        private void CmdAnimateWalkOnServer(bool walking)
        {
            RpcAnimateWalkOnClients(walking);
        }

        [Command]
        private void CmdAnimateRunOnServer(bool running)
        {
            RpcAnimateRunOnClients(running);
        }

        [Command]
        private void CmdAnimateCrouchOnServer(bool crouching)
        {
            RpcAnimateCrouchOnClients(crouching);
        }

        [Command]
        private void CmdAnimateReloadOnServer(bool reloading)
        {
            RpcAnimateReloadOnClients(reloading);
        }
        
        // Server runs this on all clients
        [ClientRpc]
        private void RpcAnimateWalkOnClients(bool walking)
        {
            if (IsNetworkModel())
            {
                animator.SetBool(AnimationBools.Walking, walking);
            }
        }

        [ClientRpc]
        private void RpcAnimateRunOnClients(bool running)
        {
            if (IsNetworkModel())
            {
                animator.SetBool(AnimationBools.Running, running);
            }
        }

        [ClientRpc]
        private void RpcAnimateCrouchOnClients(bool crouching)
        {
            if (IsNetworkModel())
            {
                animator.SetBool(AnimationBools.Crouching, crouching);
            }
        }
        
        [ClientRpc]
        private void RpcAnimateReloadOnClients(bool reloading)
        {
            if (IsNetworkModel())
            {
                animator.SetBool(AnimationBools.Reloading, reloading);
            }
        }


        private bool IsNetworkModel()
        {
            return _networkModelManager == null || _networkModelManager.IsUsingNetworkModel;
        }

        public static class AnimationBools
        {
            public const string Walking = "Walking";
            public const string Running = "Running";
            public const string Crouching = "Crouching";
            public const string Reloading = "Reloading";
        }
    }
}