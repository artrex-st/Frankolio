using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour, ISceneService
{
    private Scene _additiveScenes;

    public void LoadingScene(ScreenReference sceneName)
    {
        SceneManager.LoadScene(sceneName.SceneName, LoadSceneMode.Single);
    }

    public void LoadingSceneAdditiveAsync(ScreenReference sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName.SceneName, LoadSceneMode.Additive);
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        _additiveScenes = loadedScene;
    }

    public void UnLoadAdditiveSceneAsync()
    {
        SceneManager.UnloadSceneAsync(_additiveScenes);
    }

    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName.SceneName);
    }
}
