public interface IGameDataService
{
    public GameStates CurrentGameState { get; }
    public void SetGameState(GameStates newGameState);
}
