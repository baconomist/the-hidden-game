using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class NetworkModelManager : NetworkBehaviour
    {
        public bool usingLocalModelByDefault = true;

        public GameObject localModel;
        public GameObject networkModel;

        private AnimatorController _animatorController;
        private bool _usingLocalAnimator = false;

        private void Awake()
        {
            _animatorController = GetComponent<AnimatorController>();
            
            _usingLocalAnimator = usingLocalModelByDefault;
            
            if(_usingLocalAnimator)
                _animatorController.animator = localModel.GetComponent<Animator>();
            else
                _animatorController.animator = _animatorController.GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (isLocalPlayer)
            {
                localModel.SetActive(true);
                networkModel.SetActive(false);

                if (!_usingLocalAnimator)
                    _animatorController.animator = localModel.GetComponent<Animator>();
                _usingLocalAnimator = true;
            }
            else
            {
                localModel.SetActive(false);
                networkModel.SetActive(true);

                if (_usingLocalAnimator)
                    _animatorController.animator = networkModel.GetComponent<Animator>();
                _usingLocalAnimator = false;
            }
        }
    }
}