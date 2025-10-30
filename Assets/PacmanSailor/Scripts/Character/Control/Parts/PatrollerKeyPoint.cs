using UnityEngine;

namespace PacmanSailor.Scripts.Character.Control.Parts
{
    public class PatrollerKeyPoint : MonoBehaviour
    {
        [field: SerializeField] public int Index {get; private set;}
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
        }
    }
}