using PacmanSailor.Scripts.UI.View;
using UnityEngine;

namespace PacmanSailor.Scripts.Config
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI Config")]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public MainMenuView MainMenuPrefab { get; private set; }
        [field: SerializeField] public PauseMenuView PauseMenuPrefab { get; private set; }
        [field: SerializeField] public HUDView HUDPrefab { get; private set; }
        [field: SerializeField] public LoseWindowView LoseWindowPrefab { get; private set; }
        [field: SerializeField] public WinWindowView WinWindowPrefab { get; private set; }
    }
}