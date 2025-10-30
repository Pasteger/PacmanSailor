using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSailor.Scripts.UI.View
{
    public class WinWindowView : AbstractView<WinWindowViewModel, WinWindowModel>
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _exitGameButton;

        public override void Init(WinWindowViewModel viewModel)
        {
            base.Init(viewModel);

            _nextLevelButton.onClick.AddListener(ViewModel.OnNextLevelButtonClicked);
            _exitGameButton.onClick.AddListener(ViewModel.OnExitGameButtonClicked);
        }

        public override void Dispose()
        {
            base.Dispose();

            _nextLevelButton.onClick.RemoveListener(ViewModel.OnNextLevelButtonClicked);
            _exitGameButton.onClick.RemoveListener(ViewModel.OnExitGameButtonClicked);
        }
    }
}