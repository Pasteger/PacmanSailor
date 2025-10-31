using System;
using PacmanSailor.Scripts.Character.Behaviour;
using PacmanSailor.Scripts.Core;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Movement
{
    public class MovementService : IPaused, IDisposable
    {
        private readonly ICharacterBehaviour _control;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;

        private Vector2 _direction = Vector2.up;
        private Vector2 _savedLinearVelocity;

        public MovementService(ICharacterBehaviour control, Rigidbody rigidbody, float speed)
        {
            _control = control;
            _rigidbody = rigidbody;
            _speed = speed;

            _control.OnChangeDirection += OnChangeDirection;
        }

        public void Move()
        {
            var moveVelocity = _direction * _speed;

            _rigidbody.linearVelocity = new Vector3(moveVelocity.x, 0, moveVelocity.y);
            Rotate(moveVelocity);
        }

        public void Pause(bool isPause)
        {
            if (isPause)
            {
                _savedLinearVelocity = _rigidbody.linearVelocity;
                _rigidbody.linearVelocity = Vector3.zero;
            }
            else
            {
                _rigidbody.linearVelocity = _savedLinearVelocity;
            }
        }

        public void Dispose() => _control.OnChangeDirection -= OnChangeDirection;

        private void OnChangeDirection(Vector2 direction) => _direction = direction;

        private void Rotate(Vector2 movement)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                if (movement.x > 0)
                {
                    _rigidbody.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else if (movement.x < 0)
                {
                    _rigidbody.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
            else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                if (movement.y > 0)
                {
                    _rigidbody.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (movement.y < 0)
                {
                    _rigidbody.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
}