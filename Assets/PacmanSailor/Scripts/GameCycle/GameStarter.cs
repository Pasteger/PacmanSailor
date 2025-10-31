using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Service;
using PacmanSailor.Scripts.Level;
using PacmanSailor.Scripts.UI.Service;
using UniRx;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameStarter : BaseGameCycle
    {
        private readonly LevelConstructor _levelConstructor;
        private readonly CharactersService _charactersService;
        private readonly UIInstaller _uiInstaller;

        public GameStarter(LevelConstructor levelConstructor, CharactersService charactersService,
            UIInstaller uiInstaller)
        {
            _levelConstructor = levelConstructor;
            _charactersService = charactersService;
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
            _charactersService.SpawnCharacters();
            _charactersService.ActivateCharacters();

            _uiInstaller.HUDModel.Score.Value = 0;
            _uiInstaller.HUDModel.Open();
        }

        private void RestartGame()
        {
            _charactersService.DestroyCharacters();
            _levelConstructor.DestroyLevel();

            _uiInstaller.PauseMenuModel.Close();
            _uiInstaller.LoseWindowModel.Close();
            _uiInstaller.WinWindowModel.Close();

            StartGame();
        }
    }
}