using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuController : BaseScreen
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _loadingButton;
    [SerializeField] private ScreenReference _gameScreenRef;
    //[SerializeField] private ScreenReference _gameMenuScreenRef;
    [SerializeField] private ScreenReference _settingsScreenRef;
    [SerializeField] private ScreenReference _loadingScreenRef;

    private void Start()
    {
        Initialize();
    }

    private new void Initialize()
    {
        base.Initialize();
        _gameButton.onClick.AddListener(HandlerGameClick);
        _settingsButton.onClick.AddListener(HandlerSettingsClick);
        _loadingButton.onClick.AddListener(HandlerLoadingClick);
    }

    private void HandlerGameClick()
    {
        ScreenService.LoadingSceneAsync(_gameScreenRef);
    }

    private void HandlerLoadingClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_loadingScreenRef);
    }

    private void HandlerSettingsClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_settingsScreenRef);
    }
}
