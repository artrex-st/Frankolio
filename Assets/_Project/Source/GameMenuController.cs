using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : BaseScreen
{
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private ScreenReference _mainMenuRef;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Dispose();
    }

    private new void Initialize()
    {
        base.Initialize();
        _mainMenuBtn.onClick.AddListener(HandlerMainMenuClick);
    }

    private void HandlerMainMenuClick()
    {
        ScreenService.LoadingSceneAsync(_mainMenuRef);
    }
}
