using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseScreen
{
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private ScreenReference _gameMenuRef;

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
        _mainMenuBtn.onClick.AddListener(HandlerGameMenuClick);
    }

    private void HandlerGameMenuClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_gameMenuRef);
    }
}
