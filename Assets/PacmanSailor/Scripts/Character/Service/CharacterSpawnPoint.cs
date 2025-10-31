using PacmanSailor.Scripts.Enum;
using UnityEngine;

namespace PacmanSailor.Scripts.Character.Service
{
    public class CharacterSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public CharacterType CharacterType { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = CharacterType switch
            {
                CharacterType.Pacman => Color.yellow,
                CharacterType.Stalker => Color.red,
                CharacterType.Patroller => Color.blue,
                CharacterType.Ambusher => Color.pink
            };
            Gizmos.DrawSphere(transform.position, transform.localScale.x / 2);
        }
    }
}
