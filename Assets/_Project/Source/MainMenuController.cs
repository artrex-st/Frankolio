using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private ScreenReference _settingsScreenRef;
    private ISceneService _sceneService;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _settingsButton.onClick.AddListener(HandlerSettingsClick);
        _sceneService = ServiceLocator.Instance.GetService<ISceneService>();
    }

    private void HandlerSettingsClick()
    {
        _sceneService.LoadingSceneAdditiveAsync(_settingsScreenRef);
    }
}
