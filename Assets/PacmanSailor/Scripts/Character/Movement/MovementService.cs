using System;
using PacmanSailor.Scripts.Character.Control;
using PacmanSailor.Scripts.Core;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Movement
{
    public class MovementService : IPaused, IDisposable
    {
        private readonly ICharacterControl _control;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;

        private Vector2 _direction = Vector2.up;
        private Vector2 _savedLinearVelocity;

        public MovementService(ICharacterControl control, Rigidbody rigidbody, float speed)
        {
            _control = control;
            _rigidbody = rigidbody;
            _speed = speed;

            _control.OnChangeDirection += OnChangeDirection;
        }

        private void OnChangeDirection(Vector2 direction) => _direction = direction;

        public void Move()
        {
            var moveVelocity = _direction * _speed;

            _rigidbody.linearVelocity = new Vector3(moveVelocity.x, 0, moveVelocity.y);
            Rotate(moveVelocity);
        }

        private void Rotate(Vector2 movement)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                _rigidbody.transform.rotation = movement.x switch
                {
                    > 0 => Quaternion.Euler(0, 90, 0),
                    < 0 => Quaternion.Euler(0, -90, 0)
                };
            }
            else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                _rigidbody.transform.rotation = movement.y switch
                {
                    > 0 => Quaternion.Euler(0, 0, 0),
                    < 0 => Quaternion.Euler(0, 180, 0)
                };
            }
        }
        
        public void Pause()
        {
            _savedLinearVelocity = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = Vector3.zero;
        }

        public void Resume() => _rigidbody.linearVelocity = _savedLinearVelocity;
        public void Dispose() => _control.OnChangeDirection -= OnChangeDirection;
    }
}