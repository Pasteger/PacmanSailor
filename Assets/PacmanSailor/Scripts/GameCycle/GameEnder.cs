using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Characters;
using PacmanSailor.Scripts.Character.Management;
using PacmanSailor.Scripts.Items.Management;
using PacmanSailor.Scripts.UI.Management;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GameEnder : AbstractGameCycle
    {
        private readonly UIInstaller _uiInstaller;
        private readonly CharactersManager _charactersManager;

        public GameEnder(UIInstaller uiInstaller, CharactersManager charactersManager)
        {
            _uiInstaller = uiInstaller;
            _charactersManager = charactersManager;
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

            PelletsManager.OnAllPelletsCollect
                .Subscribe(_ => GameWin())
                .AddTo(Disposable);
        }

        private void GameWin()
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 0) + 1);

            _charactersManager.Pause();

            _uiInstaller.HUDModel.Close();
            _uiInstaller.WinWindowModel.Open();
        }

        private void GameLose()
        {
            _charactersManager.Pause();

            _uiInstaller.HUDModel.Close();
            _uiInstaller.LoseWindowModel.Open();
        }

        private void ExitGame() => Application.Quit();
    }
}