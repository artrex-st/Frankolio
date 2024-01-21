using System.Collections.Generic;

public interface IScreenService
{
    public void Initialize(ScreenReference startupScreen, ScreenReference loadingScreen, float fakeLoadingTime);
    public void LoadingSceneAsync(ScreenReference sceneName);
    public void LoadingScenesAsync(List<ScreenReference> scenesName);
    public void LoadingSceneAdditiveAsync(ScreenReference sceneName);
    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName);
}
