using PacmanSailor.Scripts.UI.Model;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.UI.ViewModel
{
    public class HUDViewModel : AbstractViewModel<HUDModel>
    {
        public readonly ReactiveProperty<int> Score = new();

        public HUDViewModel(HUDModel model) : base(model)
        {
            model.Score
                .Subscribe(_ => Score.Value = model.Score.Value)
                .AddTo(Disposable);
        }

        public void OnPauseGameButtonClicked() => Model.OnPauseGameButtonClicked();
        public void OnJoystickChangeDirection(Vector2 direction) => Model.OnJoystickChangeDirection(direction);
    }
}