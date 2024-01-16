using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : ISceneService
{
    public void LoadingScene(ScreenReference sceneName)
    {
        SceneManager.LoadScene(sceneName.SceneName, LoadSceneMode.Single);
    }

    public void PrintSceneName()
    {
        Debug.Log($"This active scene Name is: {SceneManager.GetActiveScene().name}");
    }
}
