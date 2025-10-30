using PacmanSailor.Scripts.Items;
using UnityEngine;

namespace PacmanSailor.Scripts.Level
{
    public class PelletsGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private Pellet _pelletPrefab;
        [SerializeField] private Transform _pelletParent;

        [ContextMenu("Generate Pellets")]
        public void Generate()
        {
            for (var x = _startPosition.x; x < _endPosition.x; x++)
            {
                for (var y = _startPosition.y; y < _endPosition.y; y++)
                {
                    var pellet = Instantiate(_pelletPrefab, _pelletParent);
                    pellet.transform.position = new Vector3(x, 0.5f, y);
                }
            }
        }
    }
}