using PacmanSailor.Scripts.Character.Behaviour;
using PacmanSailor.Scripts.Character.Movement;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Pacman : BaseCharacter<MovementService, PlayerInput>
    {
        public static readonly Subject<Unit> OnHit = new();

        public override void Activate()
        {
            Behaviour = new PlayerInput(transform);
            MovementService = new MovementService(Behaviour, GetComponent<Rigidbody>(), CharacterData.Speed);
            base.Activate();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy")) OnHit.OnNext(Unit.Default);
        }
    }
}