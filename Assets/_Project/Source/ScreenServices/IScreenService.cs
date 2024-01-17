public interface IScreenService
{
    public void LoadingScene(ScreenReference sceneName);
    public void LoadingSceneAdditiveAsync(ScreenReference sceneName);
    public void UnLoadAdditiveSceneAsync();
    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName);
}
