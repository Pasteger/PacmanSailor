using System;
using System.Collections.Generic;
using PacmanSailor.Scripts.Character.Control.Modules;
using PacmanSailor.Scripts.Character.Control.States;
using PacmanSailor.Scripts.Enum;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Control
{
    public class AmbusherControl : ICharacterControl
    {
        public event Action<Vector2> OnChangeDirection;

        private readonly int _timeEyelessStalkingLimit;
        private readonly PlayerTrigger _playerTrigger;

        private readonly Dictionary<EnemyStates, IEnemyControlState> _states;
        private IEnemyControlState _currentState;
        private EnemyStates _currentStateType;

        private DateTime _timeLastVisionPlayer;

        public AmbusherControl(Vector3[] ambushPoints, NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            PlayerTrigger playerTrigger, Transform player, int timeEyelessStalkingLimit)
        {
            _states = new Dictionary<EnemyStates, IEnemyControlState>
            {
                {
                    EnemyStates.Working,
                    new AmbushingControlState(navMeshAgent, navMeshAgentRoot, ambushPoints)
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

            _playerTrigger = playerTrigger;
            _timeEyelessStalkingLimit = timeEyelessStalkingLimit;

            _playerTrigger.OnTriggered += SetPlayerPresence;

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
            if (CheckTimeOut()) ChangeState(EnemyStates.Working);
        }

        private void SetPlayerPresence()
        {
            _timeLastVisionPlayer = DateTime.Now;
            if (_currentStateType == EnemyStates.Working)
                ChangeState(EnemyStates.Stalking);
        }

        private bool CheckTimeOut() => (DateTime.Now - _timeLastVisionPlayer).TotalSeconds >= _timeEyelessStalkingLimit;

        private void ChangeDirection(Vector2 direction) => OnChangeDirection?.Invoke(direction);

        private void ChangeState(EnemyStates state)
        {
            _currentStateType = state;
            _currentState = _states[state];
        }

        public void Dispose()
        {
            foreach (var state in _states.Values)
            {
                state.OnChangeDirection -= ChangeDirection;
            }

            _playerTrigger.OnTriggered -= SetPlayerPresence;
        }
    }
}