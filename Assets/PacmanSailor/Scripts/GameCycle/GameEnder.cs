using PacmanSailor.Scripts.Character.Characters;
using PacmanSailor.Scripts.Character.Service;
using PacmanSailor.Scripts.Items.Service;
using PacmanSailor.Scripts.UI.Service;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameEnder : BaseGameCycle
    {
        private readonly UIInstaller _uiInstaller;
        private readonly CharactersService _charactersService;

        public GameEnder(UIInstaller uiInstaller, CharactersService charactersService)
        {
            _uiInstaller = uiInstaller;
            _charactersService = charactersService;
        }

        public override void Initialize()
        {
            _uiInstaller.PauseMenuModel.OnExitGame
                .Subscribe(_ => ExitGame())
                .AddTo(Disposable);

            _uiInstaller.LoseWindowModel.OnExitGame
                .Subscribe(_ => ExitGame())
                .AddTo(Disposable);

            _uiInstaller.WinWindowModel.OnExitGame
                .Subscribe(_ => ExitGame())
                .AddTo(Disposable);

            Pacman.OnHit
                .Subscribe(_ => GameLose())
                .AddTo(Disposable);

            PelletsService.OnAllPelletsCollect
                .Subscribe(_ => GameWin())
                .AddTo(Disposable);
        }

        private void GameWin()
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 0) + 1);

            _charactersService.Pause(true);

            _uiInstaller.HUDModel.Close();
            _uiInstaller.WinWindowModel.Open();
        }

        private void GameLose()
        {
            _charactersService.Pause(true);

            _uiInstaller.HUDModel.Close();
            _uiInstaller.LoseWindowModel.Open();
        }

        private void ExitGame() => Application.Quit();
    }
}