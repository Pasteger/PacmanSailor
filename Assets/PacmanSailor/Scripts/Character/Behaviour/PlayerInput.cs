using System;
using PacmanSailor.Scripts.UI.Model;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PacmanSailor.Scripts.Character.Behaviour
{
    public class PlayerInput : ICharacterBehaviour
    {
        public event Action<Vector2> OnChangeDirection;

        private readonly Transform _transform;

        private readonly InputSystem _inputSystem = new();

        private readonly CompositeDisposable _disposable = new();

        public PlayerInput(Transform transform)
        {
            _transform = transform;

            _inputSystem.Player.Up.performed += OnChangeDirectionUp;
            _inputSystem.Player.Down.performed += OnChangeDirectionDown;
            _inputSystem.Player.Right.performed += OnChangeDirectionRight;
            _inputSystem.Player.Left.performed += OnChangeDirectionLeft;

            HUDModel.OnDirectionChange
                .Subscribe(ChangeDirection)
                .AddTo(_disposable);
        }

        public void Start() => _inputSystem.Enable();

        public void Dispose()
        {
            _disposable.Dispose();

            _inputSystem.Player.Up.performed -= OnChangeDirectionUp;
            _inputSystem.Player.Down.performed -= OnChangeDirectionDown;
            _inputSystem.Player.Right.performed -= OnChangeDirectionRight;
            _inputSystem.Player.Left.performed -= OnChangeDirectionLeft;

            _inputSystem.Disable();
        }

        private void OnChangeDirectionDown(InputAction.CallbackContext _) => ChangeDirection(Vector2.down);

        private void OnChangeDirectionUp(InputAction.CallbackContext _) => ChangeDirection(Vector2.up);

        private void OnChangeDirectionRight(InputAction.CallbackContext _) => ChangeDirection(Vector2.right);

        private void OnChangeDirectionLeft(InputAction.CallbackContext _) => ChangeDirection(Vector2.left);

        private void ChangeDirection(Vector2 direction)
        {
            if (!CheckWall(direction))
                OnChangeDirection?.Invoke(direction);
        }

        private bool CheckWall(Vector2 direction)
        {
            Physics.Raycast(_transform.position, new Vector3(direction.x, 0, direction.y), out var hit, 1);
            return hit.collider != null && hit.collider.CompareTag("Wall");
        }
    }
}