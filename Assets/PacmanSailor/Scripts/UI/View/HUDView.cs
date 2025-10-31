using PacmanSailor.Scripts.UI.Components;
using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace PacmanSailor.Scripts.UI.View
{
    public class HUDView : BaseView<HUDViewModel, HUDModel>
    {
        [SerializeField] private Button _pauseGameButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Joystick _joystick;

        public override void Init(HUDViewModel viewModel)
        {
            base.Init(viewModel);

            _pauseGameButton.onClick.AddListener(ViewModel.OnPauseGameButtonClicked);

            ViewModel.Score
                .Subscribe(SetScoreText)
                .AddTo(Disposable);

            _joystick.OnSelectDirection
                .Subscribe(ViewModel.OnJoystickChangeDirection)
                .AddTo(Disposable);
        }

        private void SetScoreText(int score) => _scoreText.text = score.ToString();

        public override void Dispose()
        {
            base.Dispose();

            _pauseGameButton.onClick.RemoveListener(ViewModel.OnPauseGameButtonClicked);
        }
    }
}