using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character.Behaviour.Modules;
using PacmanSailor.Scripts.Character.Behaviour.States;
using PacmanSailor.Scripts.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour
{
    public class AmbusherBehaviour : ICharacterBehaviour
    {
        public event Action<Vector2> OnChangeDirection;

        private readonly int _timeEyelessStalkingLimit;
        private readonly PlayerTrigger _playerTrigger;

        private readonly Dictionary<EnemyState, IEnemyControlState> _states;
        private IEnemyControlState _currentState;
        private EnemyState _currentStateType;

        private DateTime _timeLastVisionPlayer;

        public AmbusherBehaviour(Vector3[] ambushPoints, NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            PlayerTrigger playerTrigger, Transform player, int timeEyelessStalkingLimit)
        {
            _states = new Dictionary<EnemyState, IEnemyControlState>
            {
                {
                    EnemyState.Work,
                    new AmbushingControlState(navMeshAgent, navMeshAgentRoot, ambushPoints)
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

            _playerTrigger = playerTrigger;
            _timeEyelessStalkingLimit = timeEyelessStalkingLimit;

            _playerTrigger.OnTriggered += SetPlayerPresence;

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

            _playerTrigger.OnTriggered -= SetPlayerPresence;
        }
        
        private void SelectState()
        {
            if (CheckTimeOut()) ChangeState(EnemyState.Work);
        }

        private void SetPlayerPresence()
        {
            _timeLastVisionPlayer = DateTime.Now;
            if (_currentStateType == EnemyState.Work)
                ChangeState(EnemyState.Stalk);
        }

        private bool CheckTimeOut() => (DateTime.Now - _timeLastVisionPlayer).TotalSeconds >= _timeEyelessStalkingLimit;

        private void ChangeDirection(Vector2 direction) => OnChangeDirection?.Invoke(direction);

        private void ChangeState(EnemyState state)
        {
            _currentStateType = state;
            _currentState = _states[state];
        }
    }
}