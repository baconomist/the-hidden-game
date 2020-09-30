using System;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    public class TheHiddenAnimatorController : AnimatorController
    {
        private InputMaster _inputMaster;

        [Client]
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
            }

            _inputMaster.Enable();

            _inputMaster.TheHiddenAttackController.PrimaryAttack.performed += (ctx) => AnimatePrimaryAttack();
            _inputMaster.TheHiddenAttackController.SecondaryAttack.performed += (ctx) => AnimateSecondaryAttack();
        }

        [Client]
        private void OnDisable()
        {
            if (hasAuthority)
                _inputMaster.Disable();
        }

        // Runs on server
        [Command]
        private void AnimatePrimaryAttack()
        {
            AnimatePrimaryAttackRpc();
        }
        
        // Runs on server
        [Command]
        private void AnimateSecondaryAttack()
        {
            OnSecondaryAttackRpc();
        }

        // Server runs this on all clients
        [ClientRpc]
        private void AnimatePrimaryAttackRpc()
        {
            animator.ResetTrigger(AnimationTriggers.PrimaryAttack);
            animator.SetTrigger(AnimationTriggers.PrimaryAttack);
        }

        [Client]
        private void OnSecondaryAttackRpc()
        {
            animator.ResetTrigger(AnimationTriggers.PrimaryAttack);
            animator.SetTrigger(AnimationTriggers.SecondaryAttack);
        }

        public static class AnimationTriggers
        {
            public const string PrimaryAttack = "Primary Attack";
            public const string SecondaryAttack = "Secondary Attack";
        }
    }
}