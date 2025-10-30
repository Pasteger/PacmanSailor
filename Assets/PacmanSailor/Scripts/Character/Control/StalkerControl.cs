using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Control
{
    public class StalkerControl : AbstractEnemyControl
    {
        private readonly Transform _player;

        public StalkerControl(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot, Transform player) :
            base(navMeshAgent, navMeshAgentRoot) => _player = player;

        protected override void SetNavMeshAgentDestination() => NavMeshAgent.SetDestination(_player.position);
    }
}