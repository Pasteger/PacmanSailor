using System;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Behaviour.States
{
    public interface IEnemyControlState
    {
        public event Action<Vector2> OnChangeDirection;
        public void Start();
        public void Update();
    }
}