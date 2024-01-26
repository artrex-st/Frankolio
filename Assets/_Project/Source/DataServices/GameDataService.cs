using UnityEngine;

public enum GameStates
{
    GameRunning,
    GamePaused,
    GameWaiting
}

public struct RequestNewGameStateEvent : IEvent
{
    public readonly GameStates CurrentGameState;

    public RequestNewGameStateEvent(GameStates currentGameState)
    {
        CurrentGameState = currentGameState;
    }
}

public class GameDataService : MonoBehaviour, IGameDataService
{
    public GameStates CurrentGameState { get; private set; } = GameStates.GameWaiting;

    public void SetGameState(GameStates newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            return;
        }

        CurrentGameState = newGameState;
        new RequestNewGameStateEvent(CurrentGameState).Invoke();
    }
}
