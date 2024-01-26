using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseScreen
{
    [SerializeField] private Button _mainMenuBtn;
    [SerializeField] private ScreenReference _gameMenuRef;
    private IGameDataService _gameDataService;

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
        _gameDataService = ServiceLocator.Instance.GetService<IGameDataService>();
        _mainMenuBtn.onClick.AddListener(HandlerGameMenuClick);
        StartGame();
    }

    private async void StartGame()
    {
        //TODO: Finish Loading screen fade effect time using DOTween
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        new RequestNewGameStateEvent(GameStates.GameRunning).Invoke();
    }

    private void HandlerGameMenuClick()
    {
        ScreenService.LoadingSceneAdditiveAsync(_gameMenuRef);
        new RequestNewGameStateEvent(GameStates.GamePaused).Invoke();
    }
}
