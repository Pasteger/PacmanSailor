using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public class LoseWindowModel : AbstractModel
    {
        public readonly Subject<Unit> OnRestartGame = new();
        public readonly Subject<Unit> OnExitGame = new();

        public void OnRestartGameButtonClicked() => OnRestartGame.OnNext(Unit.Default);
        public void OnExitGameButtonClicked() => OnExitGame.OnNext(Unit.Default);
    }
}
