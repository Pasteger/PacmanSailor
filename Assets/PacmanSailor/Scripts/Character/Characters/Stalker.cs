using PacmanSailor.Scripts.Character.Behaviour;
using PacmanSailor.Scripts.Character.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Stalker : BaseCharacter<MovementService, StalkerBehaviour>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _navMeshAgentRoot;
        private Transform _player;

        public override void Activate()
        {
            _player = FindAnyObjectByType<Pacman>().transform;
            
            Behaviour = new StalkerBehaviour(_navMeshAgent, _navMeshAgentRoot, _player);
            MovementService = new MovementService(Behaviour, GetComponent<Rigidbody>(), CharacterData.Speed);

            base.Activate();
        }

        protected override void FixedUpdate()
        {
            Behaviour.Update();
            base.FixedUpdate();
        }
    }
}