using System;
using Game;
using Mirror;
using UnityEngine;

namespace DefaultNamespace
{
    public class FpsCameraController : NetworkBehaviour
    {
        public bool lockCursor = true;
        public Vector2 verticalRotBounds = new Vector2(40, 270);

        private InputMaster _inputMasterInstance;

        private Camera _camera;
        private Vector2 _mouseDelta = Vector2.zero;

        private InputMaster _inputMaster
        {
            get
            {
                if (_inputMasterInstance == null)
                    _inputMasterInstance = new InputMaster();
                return _inputMasterInstance;
            }
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            foreach (Camera cam in GetComponentsInChildren<Camera>())
                if (cam.CompareTag(Tags.MainCamera))
                    _camera = cam;

            _inputMaster.Enable();
            _inputMaster.FpsController.Look.performed += (ctx) => OnLook(ctx.ReadValue<Vector2>());
        }

        private void OnDisable()
        {
            if (isLocalPlayer)
                _inputMaster.Disable();
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                if (lockCursor)
                    LockCursor();
                else
                    UnlockCursor();
            }
        }

        [Client]
        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

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

        [Client]
        private void OnLook(Vector2 mouseDelta)
        {
            mouseDelta = new Vector2(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height);

            _mouseDelta = mouseDelta;
        }

        [Client]
        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        [Client]
        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}