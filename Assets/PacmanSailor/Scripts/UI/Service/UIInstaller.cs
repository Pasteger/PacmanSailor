using PacmanSailor.Scripts.Descriptors;
using PacmanSailor.Scripts.UI.Model;
using PacmanSailor.Scripts.UI.View;
using PacmanSailor.Scripts.UI.ViewModel;
using UnityEngine;

namespace PacmanSailor.Scripts.UI.Service
{
    public class UIInstaller : MonoBehaviour
    {
        [SerializeField] private UIDescriptor _config;

        public MainMenuModel MainMenuModel { get; private set; }
        public PauseMenuModel PauseMenuModel { get; private set; }
        public HUDModel HUDModel { get; private set; }
        public LoseWindowModel LoseWindowModel { get; private set; }
        public WinWindowModel WinWindowModel { get; private set; }

        public void Install(Transform canvas)
        {
            MainMenuModel = new MainMenuModel();
            PauseMenuModel = new PauseMenuModel();
            HUDModel = new HUDModel();
            LoseWindowModel = new LoseWindowModel();
            WinWindowModel = new WinWindowModel();

            var mainMenuView = Instantiate(_config.MainMenuPrefab, canvas).GetComponent<MainMenuView>();
            var pauseMenuView = Instantiate(_config.PauseMenuPrefab, canvas).GetComponent<PauseMenuView>();
            var hudView = Instantiate(_config.HUDPrefab, canvas).GetComponent<HUDView>();
            var loseWindowView = Instantiate(_config.LoseWindowPrefab, canvas).GetComponent<LoseWindowView>();
            var winWindowView = Instantiate(_config.WinWindowPrefab, canvas).GetComponent<WinWindowView>();

            mainMenuView.Init(new MainMenuViewModel(MainMenuModel));
            pauseMenuView.Init(new PauseMenuViewModel(PauseMenuModel));
            hudView.Init(new HUDViewModel(HUDModel));
            loseWindowView.Init(new LoseWindowViewModel(LoseWindowModel));
            winWindowView.Init(new WinWindowViewModel(WinWindowModel));
        }
    }
}