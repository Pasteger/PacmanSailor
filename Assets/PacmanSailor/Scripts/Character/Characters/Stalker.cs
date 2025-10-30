using PacmanSailor.Scripts.Character.Control;
using PacmanSailor.Scripts.Character.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Stalker : AbstractCharacter<MovementService, StalkerControl>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _navMeshAgentRoot;
        private Transform _player;

        public override void Activate()
        {
            _player = FindAnyObjectByType<Pacman>().transform;
            
            Control = new StalkerControl(_navMeshAgent, _navMeshAgentRoot, _player);
            MovementService = new MovementService(Control, GetComponent<Rigidbody>(), CharacterData.Speed);

            base.Activate();
        }

        protected override void FixedUpdate()
        {
            Control.Update();
            base.FixedUpdate();
        }
    }
}