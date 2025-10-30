using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public class WinWindowModel : AbstractModel
    {
        public readonly Subject<Unit> OnNextLevel = new();
        public readonly Subject<Unit> OnExitGame = new();

        public void OnNextLevelButtonClicked() => OnNextLevel.OnNext(Unit.Default);
        public void OnExitGameButtonClicked() => OnExitGame.OnNext(Unit.Default);
    }
}