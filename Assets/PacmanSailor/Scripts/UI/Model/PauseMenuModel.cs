using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public class PauseMenuModel : AbstractModel
    {
        public readonly Subject<Unit> OnResumeGame = new();
        public readonly Subject<Unit> OnRestartGame = new();
        public readonly Subject<Unit> OnExitGame = new();

        public void OnResumeGameButtonClicked() => OnResumeGame.OnNext(Unit.Default);
        public void OnRestartGameButtonClicked() => OnRestartGame.OnNext(Unit.Default);
        public void OnExitGameButtonClicked() => OnExitGame.OnNext(Unit.Default);
    }
}