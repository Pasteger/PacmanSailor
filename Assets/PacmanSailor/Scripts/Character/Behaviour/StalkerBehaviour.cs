using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour
{
    public class StalkerBehaviour : BaseEnemyBehaviour
    {
        private readonly Transform _player;

        public StalkerBehaviour(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot, Transform player) :
            base(navMeshAgent, navMeshAgentRoot) => _player = player;

        protected override void SetNavMeshAgentDestination() => NavMeshAgent.SetDestination(_player.position);
    }
}