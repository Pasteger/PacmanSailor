using PacmanSailor.Scripts.UI.Model;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public class WinWindowViewModel : AbstractViewModel<WinWindowModel>
    {
        public WinWindowViewModel(WinWindowModel model) : base(model)
        {
        }

        public void OnNextLevelButtonClicked() => Model.OnNextLevelButtonClicked();
        public void OnExitGameButtonClicked() => Model.OnExitGameButtonClicked();
    }
}