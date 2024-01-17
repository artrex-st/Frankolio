public interface ISceneService
{
    public void LoadingScene(ScreenReference sceneName);
    public void LoadingSceneAdditiveAsync(ScreenReference sceneName);
    public void UnLoadAdditiveSceneAsync();
}
