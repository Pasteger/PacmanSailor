using UniRx;

namespace PacmanSailor.Scripts.UI.Model
{
    public class MainMenuModel : BaseModel
    {
        public readonly Subject<Unit> OnStartGame = new();

        public void OnStartGameButtonClicked() => OnStartGame.OnNext(Unit.Default);
    }
}