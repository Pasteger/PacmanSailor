using System;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Control.Modules
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action OnTriggered;

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player")) OnTriggered?.Invoke();
        }
    }
}