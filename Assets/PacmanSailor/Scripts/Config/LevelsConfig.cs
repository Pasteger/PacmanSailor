using UnityEngine;

namespace PacmanSailor.Scripts.Config
{
    [CreateAssetMenu(fileName = "Levels Config", menuName = "Configs/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private GameObject[] _levelsPrefabs;

        public GameObject GetLevel(int level) => _levelsPrefabs[level];
        public int GetLevelCount() => _levelsPrefabs.Length;
    }
}