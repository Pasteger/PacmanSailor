using PacmanSailor.Scripts.Descriptors;
using UnityEngine;

namespace PacmanSailor.Scripts.Level
{
    public class LevelConstructor : MonoBehaviour
    {
        [SerializeField] private LevelsDescriptor _levelsConfig;

        private GameObject _currentLevel;

        public void Construct()
        {
            var level = PlayerPrefs.GetInt("CurrentLevel", 0);

            if (level >= _levelsConfig.GetLevelCount())
            {
                level = 0;
                PlayerPrefs.SetInt("CurrentLevel", 0);
            }

            _currentLevel = Instantiate(_levelsConfig.GetLevel(level));
        }

        public void DestroyLevel()
        {
            if (_currentLevel) Destroy(_currentLevel.gameObject);
        }
    }
}