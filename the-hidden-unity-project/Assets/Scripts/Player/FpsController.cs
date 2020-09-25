using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    // FPS Controller
    // 1. Create a Parent Object like a 3D model
    // 2. Make the Camera the user is going to use as a child and move it to the height you wish. 
    // Escape Key: Escapes the mouse lock
    // Mouse click after pressing escape will lock the mouse again

    public class FpsController : MonoBehaviour
    {
        public bool lockCursor = true;
        public Vector2 verticalRotBounds = new Vector2(40, 270);

        public float movementSpeed = 2f;

        [HideInInspector] public Vector3 movementDirection = Vector3.zero;

        private InputMaster _inputMaster;
        private Camera _camera;

        private void OnEnable()
        {
            if (_inputMaster == null)
            {
                _inputMaster = new InputMaster();
            }

            _inputMaster.Enable();
            _inputMaster.FpsController.Look.performed += (ctx) => Look(ctx.ReadValue<Vector2>());
            _inputMaster.FpsController.Movement.performed += (ctx) => UpdateMovement(ctx.ReadValue<Vector2>());
            _inputMaster.FpsController.Movement.canceled += (ctx) => StopMovement();
        }

        private void OnDisable()
        {
            _inputMaster?.Disable();
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (lockCursor)
                LockCursor();
            else
                UnlockCursor();


            if (Mathf.Abs(movementDirection.x) > 0)
            {
                transform.position += transform.right * (Mathf.Sign(movementDirection.x)
                                                         * movementSpeed * Time.deltaTime);
            }

            if (Mathf.Abs(movementDirection.z) > 0)
            {
                transform.position += transform.forward * (Mathf.Sign(movementDirection.z)
                                                         * movementSpeed * Time.deltaTime);
            }

        }

        private void Look(Vector2 mouseDelta)
        {
            mouseDelta = new Vector2(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height);

            // Vertical Rotation
            // local euler angles 0-360 deg
            Vector3 camLocalEuler = _camera.transform.localEulerAngles;
            float appliedVerticalRot = -mouseDelta.y * Time.deltaTime;

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
            transform.Rotate(new Vector3(0, mouseDelta.x, 0) * Time.deltaTime);
        }

        private void UpdateMovement(Vector2 direction)
        {
            movementDirection = new Vector3(direction.x, 0, direction.y);
        }

        private void StopMovement()
        {
            movementDirection = Vector3.zero;
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
    }
}