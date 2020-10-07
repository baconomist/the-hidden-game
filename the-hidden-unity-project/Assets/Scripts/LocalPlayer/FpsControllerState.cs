using System;
using Game;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    [DisallowMultipleComponent]
    public class FpsControllerState : NetworkBehaviour
    {
        public float groundDetectionRadius = 1;

        [HideInInspector] public bool isGrounded = false;
        [HideInInspector] public bool isClingingToWall = false;
        
        private CharacterController _characterController;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (hasAuthority)
            {
                isGrounded =
                    Physics.CheckSphere(
                        transform.position - new Vector3(0,
                            _characterController.height / 2f - _characterController.center.y, 0),
                        groundDetectionRadius, LayerMask.GetMask(Layers.Ground), QueryTriggerInteraction.Ignore);
            }
        }
    }
}