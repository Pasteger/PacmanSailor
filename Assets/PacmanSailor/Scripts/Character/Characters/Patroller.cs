using System.Linq;
using PacmanSailor.Scripts.Character.Behaviour;
using PacmanSailor.Scripts.Character.Behaviour.Parts;
using PacmanSailor.Scripts.Character.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Patroller : BaseCharacter<MovementService, PatrollerBehaviour>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _navMeshAgentRoot;
        [SerializeField] private int _timeEyelessStalkingLimit;

        public override void Activate()
        {
            var characterRigidbody = GetComponent<Rigidbody>();
            var playerTransform = FindAnyObjectByType<Pacman>().transform;
            var keyPoints = FindObjectsByType<PatrollerKeyPoint>(FindObjectsSortMode.None)
                .OrderBy(point => point.Index)
                .Select(point => point.transform.position)
                .ToArray();

            Behaviour = new PatrollerBehaviour(keyPoints, _navMeshAgent, _navMeshAgentRoot, transform, playerTransform,
                _timeEyelessStalkingLimit);
            MovementService = new MovementService(Behaviour, characterRigidbody, CharacterData.Speed);
            base.Activate();
        }

        protected override void FixedUpdate()
        {
            Behaviour.Update();
            base.FixedUpdate();
        }
    }
}