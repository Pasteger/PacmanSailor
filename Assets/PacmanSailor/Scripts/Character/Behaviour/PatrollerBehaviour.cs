using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character.Behaviour.States;
using PacmanSailor.Scripts.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour
{
    public class PatrollerBehaviour : ICharacterBehaviour
    {
        public event Action<Vector2> OnChangeDirection;

        private readonly Transform _transform;
        private readonly int _timeEyelessStalkingLimit;

        private readonly Dictionary<EnemyState, IEnemyControlState> _states;
        private IEnemyControlState _currentState;
        private EnemyState _currentStateType;

        private DateTime _timeLastVisionPlayer;

        public PatrollerBehaviour(Vector3[] keyPoints, NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            Transform transform, Transform player, int timeEyelessStalkingLimit)
        {
            _states = new Dictionary<EnemyState, IEnemyControlState>
            {
                {
                    EnemyState.Work,
                    new PatrollingControlState(navMeshAgent, navMeshAgentRoot, keyPoints)
                },
                {
                    EnemyState.Stalk,
                    new StalkingControlState(navMeshAgent, navMeshAgentRoot, player)
                }
            };

            foreach (var state in _states.Values)
            {
                state.OnChangeDirection += ChangeDirection;
            }

            _transform = transform;
            _timeEyelessStalkingLimit = timeEyelessStalkingLimit;

            ChangeState(EnemyState.Work);
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

        public void Dispose()
        {
            foreach (var state in _states.Values)
            {
                state.OnChangeDirection -= ChangeDirection;
            }
        }
        
        private void SelectState()
        {
            if (CheckPlayerPresence())
            {
                _timeLastVisionPlayer = DateTime.Now;
                if (_currentStateType == EnemyState.Work)
                    ChangeState(EnemyState.Stalk);
            }
            else if (CheckTimeOut()) ChangeState(EnemyState.Work);
        }
        
        private bool CheckTimeOut() => (DateTime.Now - _timeLastVisionPlayer).TotalSeconds >= _timeEyelessStalkingLimit;

        private void ChangeDirection(Vector2 direction) => OnChangeDirection?.Invoke(direction);

        private void ChangeState(EnemyState state)
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
    }
}