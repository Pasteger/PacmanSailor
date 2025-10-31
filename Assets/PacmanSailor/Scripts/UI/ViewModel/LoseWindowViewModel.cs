using PacmanSailor.Scripts.UI.Model;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public class LoseWindowViewModel : BaseViewModel<LoseWindowModel>
    {
        public LoseWindowViewModel(LoseWindowModel model) : base(model)
        {
        }

        public void OnRestartGameButtonClicked() => Model.OnRestartGameButtonClicked();
        public void OnExitGameButtonClicked() => Model.OnExitGameButtonClicked();
    }
}