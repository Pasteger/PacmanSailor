using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSailor.Scripts.UI.View
{
    public class MainMenuView : AbstractView<MainMenuViewModel, MainMenuModel>
    {
        [SerializeField] private Button _startGameButton;

        public override void Init(MainMenuViewModel viewModel)
        {
            base.Init(viewModel);

            _startGameButton.onClick.AddListener(ViewModel.OnStartGameButtonClicked);
        }

        public override void Dispose()
        {
            base.Dispose();

            _startGameButton.onClick.RemoveListener(ViewModel.OnStartGameButtonClicked);
        }
    }
}