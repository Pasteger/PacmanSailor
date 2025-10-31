using PacmanSailor.Scripts.UI.Model;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public class MainMenuViewModel : BaseViewModel<MainMenuModel>
    {
        public MainMenuViewModel(MainMenuModel model) : base(model)
        {
        }

        public void OnStartGameButtonClicked() => Model.OnStartGameButtonClicked();
    }
}