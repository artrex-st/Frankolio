using Cysharp.Threading.Tasks;
using System;
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
        StartGame();
    }

    private async void StartGame()
    {
        //TODO: Finish Loading screen fade effect time using DOTween
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        new RequestGameStateUpdateEvent(GameStates.GameRunning).Invoke();
    }

    private void HandlerGameMenuClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_gameMenuRef);
        new RequestGameStateUpdateEvent(GameStates.GamePaused).Invoke();
    }
}
