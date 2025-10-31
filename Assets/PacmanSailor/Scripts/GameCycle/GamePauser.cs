using PacmanSailor.Scripts.Character;
using PacmanSailor.Scripts.Character.Service;
using PacmanSailor.Scripts.UI.Service;
using UniRx;

namespace PacmanSailor.Scripts.GameCycle
{
    public class GamePauser : BaseGameCycle
    {
        private readonly UIInstaller _uiInstaller;
        private readonly CharactersService _charactersService;

        public GamePauser(UIInstaller uiInstaller, CharactersService charactersService)
        {
            _uiInstaller = uiInstaller;
            _charactersService = charactersService;
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
            _charactersService.Pause(true);

            _uiInstaller.HUDModel.Close();
            _uiInstaller.PauseMenuModel.Open();
        }

        private void ResumeGame()
        {
            _charactersService.Pause(false);

            _uiInstaller.PauseMenuModel.Close();
            _uiInstaller.HUDModel.Open();
        }
    }
}