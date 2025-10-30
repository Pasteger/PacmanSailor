using System;
using PacmanSailor.Scripts.Character.Control;
using PacmanSailor.Scripts.Character.Movement;
using PacmanSailor.Scripts.Data;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Characters
{
    public abstract class AbstractCharacter<TM, TC> : MonoBehaviour, ICharacter
        where TM : MovementService where TC : ICharacterControl, IDisposable
    {
        protected TM MovementService;
        protected TC Control;
        protected CharacterData CharacterData { get; private set; }

        public void Initialize(CharacterData characterData) => CharacterData = characterData;
        public virtual void Activate() => Control.Start();

        public void Destroy() => Destroy(gameObject);
        
        protected virtual void FixedUpdate() => MovementService.Move();

        protected virtual void OnDestroy() => Dispose();

        protected virtual void Dispose()
        {
            MovementService.Dispose();
            Control.Dispose();
        }

        public virtual void Pause()
        {
            enabled = false;
            MovementService.Pause();
        }

        public virtual void Resume()
        {
            enabled = true;
            MovementService.Resume();
        }
    }
}