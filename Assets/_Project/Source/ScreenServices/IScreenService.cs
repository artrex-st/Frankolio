public interface IScreenService
{
    public void Initialize(ScreenReference loadingScreen);
    public void LoadingScene(ScreenReference sceneName);
    public void LoadingSceneAdditiveAsync(ScreenReference sceneName);
    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName);
}
