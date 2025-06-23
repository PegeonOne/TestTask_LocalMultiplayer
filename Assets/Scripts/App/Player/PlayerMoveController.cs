using Game.Core;
using Unity.Netcode;
using UnityEngine;

namespace Game.App.Player
{
    public sealed class PlayerMoveController : BaseBehavior
    {
        [SerializeField] private float _mouseSensitivity = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Camera _camera;

        private CharacterController _controller;
        private float _rotationY = 0f;

        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;
        private Vector2 _lookInput;

        protected override void Initialise()
        {
            _controller = GetComponent<CharacterController>();
            if(!IsOwner) _camera.gameObject.SetActive(false);

            _inputActions = new PlayerInputActions();
            _inputActions.Enable();

            _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

            _inputActions.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
            _inputActions.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
        }

        protected override void OnUpdate()
        {
            if(!IsOwner) return;
            Player_MovingAndRotation_ServerRpc(_moveInput, _lookInput);
        }

        [ServerRpc(RequireOwnership = false)]
        private void Player_MovingAndRotation_ServerRpc(Vector2 moveInput, Vector2 lookInput)
        {
            _rotationY += lookInput.x * _mouseSensitivity * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, _rotationY, 0f);

            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 moveDirection = transform.right * direction.x + transform.forward * direction.z;
            _controller.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);
        }

        protected override void OnDispose()
        {
            _inputActions?.Disable();
            _inputActions = null;
            _controller = null;
        }
    }
}
