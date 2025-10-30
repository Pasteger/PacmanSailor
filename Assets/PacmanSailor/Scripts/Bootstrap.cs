using PacmanSailor.Scripts.Character.Management;
using PacmanSailor.Scripts.GameCycle;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Management;
using UnityEngine;

namespace PacmanSailor.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelConstructor _levelConstructorPrefab;
        [SerializeField] private CharactersManager _characterManagerPrefab;
        [SerializeField] private UIInstaller _uiInstallerPrefab;

        [SerializeField] private Transform _canvas;

        private GameCycleController _gameCycleController;

        private void Awake()
        {
            _gameCycleController = new GameCycleController(_uiInstallerPrefab, _levelConstructorPrefab,
                _characterManagerPrefab, _canvas);
        }

        private void OnDestroy() => _gameCycleController.Dispose();
    }
}