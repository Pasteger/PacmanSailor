using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSailor.Scripts.UI.View
{
    public class PauseMenuView : BaseView<PauseMenuViewModel, PauseMenuModel>
    {
        [SerializeField] private Button _resumeGameButton;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _exitGameButton;

        public override void Init(PauseMenuViewModel viewModel)
        {
            base.Init(viewModel);

            _resumeGameButton.onClick.AddListener(ViewModel.OnResumeGameButtonClicked);
            _restartGameButton.onClick.AddListener(ViewModel.OnRestartGameButtonClicked);
            _exitGameButton.onClick.AddListener(ViewModel.OnExitGameButtonClicked);
        }

        public override void Dispose()
        {
            base.Dispose();

            _resumeGameButton.onClick.RemoveListener(ViewModel.OnResumeGameButtonClicked);
            _restartGameButton.onClick.RemoveListener(ViewModel.OnRestartGameButtonClicked);
            _exitGameButton.onClick.RemoveListener(ViewModel.OnExitGameButtonClicked);
        }
    }
}