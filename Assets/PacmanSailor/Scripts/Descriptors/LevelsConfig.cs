using UnityEngine;

namespace PacmanSailor.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "Levels Descriptor", menuName = "Descriptors/Levels Descriptor")]
    public class LevelsDescriptor : ScriptableObject
    {
        [SerializeField] private GameObject[] _levelsPrefabs;

        public GameObject GetLevel(int level) => _levelsPrefabs[level];
        public int GetLevelCount() => _levelsPrefabs.Length;
    }
}