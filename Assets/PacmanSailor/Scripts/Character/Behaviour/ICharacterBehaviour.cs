using System;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Behaviour
{
    public interface ICharacterBehaviour : IDisposable
    {
        public event Action<Vector2> OnChangeDirection;

        public void Start();
    }
}