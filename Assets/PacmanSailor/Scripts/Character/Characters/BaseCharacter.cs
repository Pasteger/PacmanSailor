using System;
using PacmanSailor.Scripts.Character.Behaviour;
using PacmanSailor.Scripts.Character.Movement;
using PacmanSailor.Scripts.Data;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Characters
{
    public abstract class BaseCharacter<TM, TC> : MonoBehaviour, ICharacter
        where TM : MovementService where TC : ICharacterBehaviour, IDisposable
    {
        protected TM MovementService;
        protected TC Behaviour;
        protected CharacterData CharacterData { get; private set; }

        public void Initialize(CharacterData characterData) => CharacterData = characterData;
        public virtual void Activate() => Behaviour.Start();

        public virtual void Pause(bool isPause)
        {
            enabled = !isPause;
            MovementService.Pause(isPause);
        }

        public void Destroy() => Destroy(gameObject);

        protected virtual void FixedUpdate() => MovementService.Move();

        protected virtual void OnDestroy() => Dispose();

        protected virtual void Dispose()
        {
            MovementService.Dispose();
            Behaviour.Dispose();
        }
    }
}