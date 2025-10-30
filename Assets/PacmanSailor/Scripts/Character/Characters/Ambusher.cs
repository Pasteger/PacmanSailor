using System.Linq;
using PacmanSailor.Scripts.Character.Control;
using PacmanSailor.Scripts.Character.Control.Modules;
using PacmanSailor.Scripts.Character.Control.Parts;
using PacmanSailor.Scripts.Character.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Ambusher : AbstractCharacter<MovementService, AmbusherControl>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _navMeshAgentRoot;
        [SerializeField] private PlayerTrigger _playerTrigger;
        [SerializeField] private int _timeEyelessStalkingLimit;

        public override void Activate()
        {
            var characterRigidbody = GetComponent<Rigidbody>();
            var playerTransform = FindAnyObjectByType<Pacman>().transform;
            var ambushPoints = FindObjectsByType<AmbushPoint>(FindObjectsSortMode.None)
                .Select(point => point.transform.position)
                .ToArray();

            Control = new AmbusherControl(ambushPoints, _navMeshAgent, _navMeshAgentRoot, _playerTrigger,
                playerTransform, _timeEyelessStalkingLimit);
            MovementService = new MovementService(Control, characterRigidbody, CharacterData.Speed);
            base.Activate();
        }

        protected override void FixedUpdate()
        {
            Control.Update();
            base.FixedUpdate();
        }
    }
}