using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public class LoseWindowModel : BaseModel
    {
        public readonly Subject<Unit> OnRestartGame = new();
        public readonly Subject<Unit> OnExitGame = new();

        public void OnRestartGameButtonClicked() => OnRestartGame.OnNext(Unit.Default);
        public void OnExitGameButtonClicked() => OnExitGame.OnNext(Unit.Default);
    }
}
