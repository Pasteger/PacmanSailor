using PacmanSailor.Scripts.Character.Service;
using PacmanSailor.Scripts.GameCycle;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Service;
using UnityEngine;
using UnityEngine.Serialization;

namespace PacmanSailor.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelConstructor _levelConstructorPrefab;
        [SerializeField] private CharactersService _characterServicePrefab;
        [SerializeField] private UIInstaller _uiInstallerPrefab;

        [SerializeField] private Transform _canvas;

        private GameCycleController _gameCycleController;

        private void Awake()
        {
            _gameCycleController = new GameCycleController(_uiInstallerPrefab, _levelConstructorPrefab,
                _characterServicePrefab, _canvas);
        }

        private void OnDestroy() => _gameCycleController.Dispose();
    }
}