using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSailor.Scripts.UI.View
{
    public class LoseWindowView : BaseView<LoseWindowViewModel, LoseWindowModel>
    {
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _exitGameButton;

        public override void Init(LoseWindowViewModel viewModel)
        {
            base.Init(viewModel);

            _restartGameButton.onClick.AddListener(ViewModel.OnRestartGameButtonClicked);
            _exitGameButton.onClick.AddListener(ViewModel.OnExitGameButtonClicked);
        }

        public override void Dispose()
        {
            base.Dispose();

            _restartGameButton.onClick.RemoveListener(ViewModel.OnRestartGameButtonClicked);
            _exitGameButton.onClick.RemoveListener(ViewModel.OnExitGameButtonClicked);
        }
    }
}