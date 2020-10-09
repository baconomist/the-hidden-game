using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class NetworkModelManager : NetworkBehaviour
    {
        public bool debugNetworkModelOnLocal = false;

        public bool usingLocalModelByDefault = true;

        public bool IsUsingNetworkModel => !isLocalPlayer || debugNetworkModelOnLocal;

        public GameObject localModel;
        public GameObject networkModel;

        private AnimatorController _animatorController;
        private bool _usingLocalAnimator = false;

        private void OnValidate()
        {
            if (IsUsingNetworkModel)
            {
                UseNetworkModel();
            }
            else
            {
                UseLocalModel();
            }
        }

        private void Awake()
        {
            _animatorController = GetComponent<AnimatorController>();

            _usingLocalAnimator = usingLocalModelByDefault;

            if (_usingLocalAnimator)
                _animatorController.animator = localModel.GetComponent<Animator>();
            else
                _animatorController.animator = _animatorController.GetComponent<Animator>();
        }

        private void Update()
        {
            if (IsUsingNetworkModel)
            {
                UseNetworkModel();
            }
            else
            {
                UseLocalModel();
            }
        }

        private void UseLocalModel()
        {
            localModel.SetActive(true);
            networkModel.SetActive(false);

            if (!_usingLocalAnimator)
                _animatorController.animator = localModel.GetComponent<Animator>();
            _usingLocalAnimator = true;
        }

        private void UseNetworkModel()
        {
            localModel.SetActive(false);
            networkModel.SetActive(true);

            if (_usingLocalAnimator)
                _animatorController.animator = networkModel.GetComponent<Animator>();
            _usingLocalAnimator = false;
        }
    }
}