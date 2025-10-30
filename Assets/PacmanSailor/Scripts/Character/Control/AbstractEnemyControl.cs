using System;
using PacmanSailor.Scripts.Character.Control.Modules;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Control
{
    public abstract class AbstractEnemyControl : ICharacterControl
    {
        public event Action<Vector2> OnChangeDirection;

        protected readonly NavMeshAgent NavMeshAgent;
        protected readonly Transform NavMeshAgentRoot;

        protected Vector2 CurrentDirection;

        protected AbstractEnemyControl(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot)
        {
            NavMeshAgent = navMeshAgent;
            NavMeshAgentRoot = navMeshAgentRoot;
        }

        public virtual void Start()
        {
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updatePosition = false;
        }

        public virtual void Update()
        {
            NavMeshAgent.nextPosition = NavMeshAgentRoot.position;
            SetNavMeshAgentDestination();
            AdjustDirection();
        }

        protected abstract void SetNavMeshAgentDestination();

        protected virtual void AdjustDirection()
        {
            if (DirectionAdjuster.AdjustDirection(NavMeshAgent.velocity, ref CurrentDirection))
                ChangeDirection();
        }

        protected void ChangeDirection() => OnChangeDirection?.Invoke(CurrentDirection);

        public void Dispose()
        {
        }
    }
}