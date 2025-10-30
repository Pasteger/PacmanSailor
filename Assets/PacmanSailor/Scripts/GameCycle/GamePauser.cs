using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Management;
using PacmanSailor.Scripts.UI.Management;
using UniRx;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GamePauser : AbstractGameCycle
    {
        private readonly UIInstaller _uiInstaller;
        private readonly CharactersManager _charactersManager;

        public GamePauser(UIInstaller uiInstaller, CharactersManager charactersManager)
        {
            _uiInstaller = uiInstaller;
            _charactersManager = charactersManager;
        }

        public override void Initialize()
        {
            _uiInstaller.HUDModel.OnPauseGame
                .Subscribe(_ => PauseGame())
                .AddTo(Disposable);
            _uiInstaller.PauseMenuModel.OnResumeGame
                .Subscribe(_ => ResumeGame())
                .AddTo(Disposable);
        }

        private void PauseGame()
        {
            _charactersManager.Pause();

            _uiInstaller.HUDModel.Close();
            _uiInstaller.PauseMenuModel.Open();
        }

        private void ResumeGame()
        {
            _charactersManager.Resume();

            _uiInstaller.PauseMenuModel.Close();
            _uiInstaller.HUDModel.Open();
        }
    }
}