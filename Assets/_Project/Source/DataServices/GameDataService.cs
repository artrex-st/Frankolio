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
            Debug.Log($"<color=Yellow>Game State Not changed!</color>");
            return;
        }

        Debug.Log($"<color=Green>Game State changed from: {CurrentGameState} to {newGameState}!</color>");
        CurrentGameState = newGameState;
        new RequestNewGameStateEvent(CurrentGameState).Invoke();
    }
}
