using UnityEngine;

namespace PacmanSailor.Scripts.Character.Behaviour.Parts
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
