using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour.States
{
    public class AmbushingControlState : BaseEnemyBehaviour, IEnemyControlState
    {
        private readonly Vector3[] _ambushPoints;

        public AmbushingControlState(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot,
            Vector3[] ambushPoints) : base(navMeshAgent, navMeshAgentRoot) => _ambushPoints = ambushPoints;

        protected override void SetNavMeshAgentDestination() => NavMeshAgent.SetDestination(GetNearestPoint());

        private Vector3 GetNearestPoint() =>
            _ambushPoints.OrderBy(p => Vector3.Distance(NavMeshAgentRoot.position, p)).First();
    }
}