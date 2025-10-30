using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character.Control.States;
using PacmanSailor.Scripts.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Control
{
    public class PatrollerControl : ICharacterControl
    {
        public event Action<Vector2> OnChangeDirection;

        private readonly Transform _transform;
        private readonly int _timeEyelessStalkingLimit;

        private readonly Dictionary<EnemyStates, IEnemyControlState> _states;
        private IEnemyControlState _currentState;
        private EnemyStates _currentStateType;

        private DateTime _timeLastVisionPlayer;

        public PatrollerControl(Vector3[] keyPoints, NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            Transform transform, Transform player, int timeEyelessStalkingLimit)
        {
            _states = new Dictionary<EnemyStates, IEnemyControlState>
            {
                {
                    EnemyStates.Working,
                    new PatrollingControlState(navMeshAgent, navMeshAgentRoot, keyPoints)
                },
                {
                    EnemyStates.Stalking,
                    new StalkingControlState(navMeshAgent, navMeshAgentRoot, player)
                }
            };

            foreach (var state in _states.Values)
            {
                state.OnChangeDirection += ChangeDirection;
            }

            _transform = transform;
            _timeEyelessStalkingLimit = timeEyelessStalkingLimit;

            ChangeState(EnemyStates.Working);
        }

        public void Start()
        {
            foreach (var state in _states.Values)
            {
                state.Start();
            }
        }

        public void Update()
        {
            SelectState();
            _currentState.Update();
        }

        private void SelectState()
        {
            if (CheckPlayerPresence())
            {
                _timeLastVisionPlayer = DateTime.Now;
                if (_currentStateType == EnemyStates.Working)
                    ChangeState(EnemyStates.Stalking);
            }
            else if (CheckTimeOut()) ChangeState(EnemyStates.Working);
        }

        private bool CheckTimeOut() => (DateTime.Now - _timeLastVisionPlayer).TotalSeconds >= _timeEyelessStalkingLimit;

        private void ChangeDirection(Vector2 direction) => OnChangeDirection?.Invoke(direction);

        private void ChangeState(EnemyStates state)
        {
            _currentStateType = state;
            _currentState = _states[state];
        }

        private bool CheckPlayerPresence() =>
            CheckHitPlayer(RaycastCheck(Vector3.forward)) ||
            CheckHitPlayer(RaycastCheck(Vector3.back)) ||
            CheckHitPlayer(RaycastCheck(Vector3.right)) ||
            CheckHitPlayer(RaycastCheck(Vector3.left));

        private RaycastHit RaycastCheck(Vector3 direction)
        {
            Physics.Raycast(_transform.position, direction, out var hit);
            return hit;
        }

        private bool CheckHitPlayer(RaycastHit hit) => hit.collider && hit.collider.CompareTag("Player");

        public void Dispose()
        {
            foreach (var state in _states.Values)
            {
                state.OnChangeDirection -= ChangeDirection;
            }
        }
    }
}