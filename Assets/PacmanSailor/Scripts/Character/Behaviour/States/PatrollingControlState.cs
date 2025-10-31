using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour.States
{
    public class PatrollingControlState : BaseEnemyBehaviour, IEnemyControlState
    {
        private readonly Vector3[] _keyPoints;
        private int _currentKeyPointIndex;

        public PatrollingControlState(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            Vector3[] keyPoints) : base(navMeshAgent, navMeshAgentRoot) => _keyPoints = keyPoints;

        protected override void SetNavMeshAgentDestination() => NavMeshAgent.SetDestination(ChangeCurrentKeyPoint());

        private Vector3 ChangeCurrentKeyPoint()
        {
            var currentKeyPoint = _keyPoints[_currentKeyPointIndex];
            if ((currentKeyPoint - NavMeshAgentRoot.position).magnitude > 1f) return currentKeyPoint;

            _currentKeyPointIndex = _currentKeyPointIndex == _keyPoints.Length - 1 ? 0 : _currentKeyPointIndex + 1;

            return _keyPoints[_currentKeyPointIndex];
        }
    }
}