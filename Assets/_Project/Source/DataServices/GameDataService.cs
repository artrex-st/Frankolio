using System;
using UnityEngine;

public enum GameStates
{
    GameRunning,
    GamePaused,
    GameWaiting
}

public struct RequestGameStateUpdateEvent : IEvent
{
    public readonly GameStates NewGameState;

    public RequestGameStateUpdateEvent(GameStates newGameState)
    {
        NewGameState = newGameState;
    }
}

public struct ResponseGameStateUpdateEvent : IEvent
{
    public readonly GameStates CurrentGameState;

    public ResponseGameStateUpdateEvent(GameStates currentGameState)
    {
        CurrentGameState = currentGameState;
    }
}

public class GameDataService : MonoBehaviour, IGameDataService
{
    public GameStates CurrentGameState { get; private set; } = GameStates.GameWaiting;

    public void Initialize()
    {
        new RequestGameStateUpdateEvent().AddListener(HandlerRequestGameStateUpdateEvent);
    }

    public void SetGameState(GameStates newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            Debug.Log($"<color=Yellow>Game State Not changed!</color>");
            return;
        }

        Debug.Log($"<color=Green>Game State changed from: {CurrentGameState} to {newGameState}!</color>");
        CurrentGameState = newGameState;
        new ResponseGameStateUpdateEvent(CurrentGameState).Invoke();
    }

    private void HandlerRequestGameStateUpdateEvent(RequestGameStateUpdateEvent e)
    {
        SetGameState(e.NewGameState);
    }
}
