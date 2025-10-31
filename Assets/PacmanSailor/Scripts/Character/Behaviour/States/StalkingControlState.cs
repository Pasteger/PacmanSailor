using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Behaviour.States
{
    public class StalkingControlState : StalkerBehaviour, IEnemyControlState
    {
        public StalkingControlState(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot, Transform player) :
            base(navMeshAgent, navMeshAgentRoot, player)
        {
        }
    }
}