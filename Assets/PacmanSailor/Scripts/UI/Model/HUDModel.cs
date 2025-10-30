using PacmanSailor.Scripts.Items.Management;
using UniRx;
using UnityEngine;

namespace PacmanSailor.Scripts.UI.Model
{
    public class HUDModel : AbstractModel
    {
        public readonly Subject<Unit> OnPauseGame = new();
        public readonly ReactiveProperty<int> Score = new();
        
        public static readonly Subject<Vector2> OnDirectionChange = new();

        public HUDModel()
        {
            PelletsManager.OnPelletCollect
                .Subscribe(_ => Score.Value++)
                .AddTo(Disposable);
        }

        public void OnPauseGameButtonClicked() => OnPauseGame.OnNext(Unit.Default);
        public void OnJoystickChangeDirection(Vector2 direction) => OnDirectionChange.OnNext(direction);
    }
}