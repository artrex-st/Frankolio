using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenService : MonoBehaviour, IScreenService
{
    private Scene _additiveScenes;
    private ScreenReference _loadingScreen;

    public void Initialize(ScreenReference loadingScreen)
    {
        _loadingScreen = loadingScreen;
    }

    public void LoadingScene(ScreenReference sceneName)
    {
        SceneManager.LoadScene(sceneName.SceneName, LoadSceneMode.Single);
    }

    public void LoadingSceneAdditiveAsync(ScreenReference sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName.SceneName, LoadSceneMode.Additive);
    }

    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName.SceneName);
    }
}
