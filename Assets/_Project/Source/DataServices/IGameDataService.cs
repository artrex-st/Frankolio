public interface IGameDataService
{
    public GameStates CurrentGameState { get; }

    public void Initialize();
    public void SetGameState(GameStates newGameState);
}
