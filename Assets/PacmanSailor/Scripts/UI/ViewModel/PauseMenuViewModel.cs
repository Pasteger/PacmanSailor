using PacmanSailor.Scripts.UI.Model;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public class PauseMenuViewModel : BaseViewModel<PauseMenuModel>
    {
        public PauseMenuViewModel(PauseMenuModel model) : base(model)
        {
        }

        public void OnResumeGameButtonClicked() => Model.OnResumeGameButtonClicked();
        public void OnRestartGameButtonClicked() => Model.OnRestartGameButtonClicked();
        public void OnExitGameButtonClicked() => Model.OnExitGameButtonClicked();
    }
}