using System;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Control
{
    public interface ICharacterControl : IDisposable
    {
        public event Action<Vector2> OnChangeDirection;

        public void Start();
    }
}