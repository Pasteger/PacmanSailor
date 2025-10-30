using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.Items
{
    public class Pellet : MonoBehaviour
    {
        public readonly Subject<Unit> OnPelletCollect = new();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            gameObject.SetActive(false);
            OnPelletCollect.OnNext(Unit.Default);
        }
    }
}