using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Management;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Management;
using UniRx;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameStarter : AbstractGameCycle
    {
        private readonly LevelConstructor _levelConstructor;
        private readonly CharactersManager _charactersManager;
        private readonly UIInstaller _uiInstaller;

        public GameStarter(LevelConstructor levelConstructor, CharactersManager charactersManager,
            UIInstaller uiInstaller)
        {
            _levelConstructor = levelConstructor;
            _charactersManager = charactersManager;
            _uiInstaller = uiInstaller;
        }

        public override void Initialize()
        {
            _uiInstaller.MainMenuModel.OnStartGame
                .Subscribe(_ => StartGame())
                .AddTo(Disposable);

            _uiInstaller.PauseMenuModel.OnRestartGame
                .Subscribe(_ => RestartGame())
                .AddTo(Disposable);

            _uiInstaller.LoseWindowModel.OnRestartGame
                .Subscribe(_ => RestartGame())
                .AddTo(Disposable);

            _uiInstaller.WinWindowModel.OnNextLevel
                .Subscribe(_ => RestartGame())
                .AddTo(Disposable);
        }

        private void StartGame()
        {
            _uiInstaller.MainMenuModel.Close();

            _levelConstructor.Construct();
            _charactersManager.SpawnCharacters();
            _charactersManager.ActivateCharacters();

            _uiInstaller.HUDModel.Score.Value = 0;
            _uiInstaller.HUDModel.Open();
        }

        private void RestartGame()
        {
            _charactersManager.DestroyCharacters();
            _levelConstructor.DestroyLevel();

            _uiInstaller.PauseMenuModel.Close();
            _uiInstaller.LoseWindowModel.Close();
            _uiInstaller.WinWindowModel.Close();

            StartGame();
        }
    }
}