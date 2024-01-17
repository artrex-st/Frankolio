using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuController : BaseScreen
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private ScreenReference _settingsScreenRef;

    private void Start()
    {
        Initialize();
    }

    private new void Initialize()
    {
        base.Initialize();
        _settingsButton.onClick.AddListener(HandlerSettingsClick);
    }

    private void HandlerSettingsClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_settingsScreenRef);
    }
}
