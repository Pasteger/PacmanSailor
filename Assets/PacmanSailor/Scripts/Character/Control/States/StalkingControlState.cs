using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Control.States
{
    public class StalkingControlState : StalkerControl, IEnemyControlState
    {
        public StalkingControlState(NavMeshAgent navMeshAgent, Transform navMeshAgentRoot, Transform player) :
            base(navMeshAgent, navMeshAgentRoot, player)
        {
        }
    }
}