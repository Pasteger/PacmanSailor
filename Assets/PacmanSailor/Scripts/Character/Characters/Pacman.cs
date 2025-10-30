using PacmanSailor.Scripts.Character.Control;
using PacmanSailor.Scripts.Character.Movement;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Characters
{
    public class Pacman : AbstractCharacter<MovementService, PlayerInput>
    {
        public static readonly Subject<Unit> OnHit = new();

        public override void Activate()
        {
            Control = new PlayerInput(transform);
            MovementService = new MovementService(Control, GetComponent<Rigidbody>(), CharacterData.Speed);
            base.Activate();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy")) OnHit.OnNext(Unit.Default);
        }
    }
}