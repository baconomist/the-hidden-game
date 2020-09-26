using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class TheHiddenAttackController : MonoBehaviour
    {
        private Animator _animator;

        private InputMaster _inputMaster;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
            }

            _inputMaster.Enable();

            _inputMaster.TheHiddenAttackController.PrimaryAttack.performed += (ctx) => OnPrimaryAttack();
            _inputMaster.TheHiddenAttackController.SecondaryAttack.performed += (ctx) => OnSecondaryAttack();
        }

        private void OnDisable()
        {
            _inputMaster.Disable();
        }

        private void OnPrimaryAttack()
        {
            _animator.SetTrigger(AnimationTriggers.PrimaryAttack);
        }

        private void OnSecondaryAttack()
        {
            _animator.SetTrigger(AnimationTriggers.SecondaryAttack);
        }

        public static class AnimationTriggers
        {
            public const string PrimaryAttack = "Primary Attack";
            public const string SecondaryAttack = "Secondary Attack";
        }
    }
}