using UnityEngine;

namespace PacmanSailor.Scripts.Character.Control.Parts
{
    public class AmbushPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.pink;
            Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
        }
    }
}
