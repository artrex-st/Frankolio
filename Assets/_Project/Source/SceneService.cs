using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : ISceneService
{
    public void LoadingScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void PrintSceneName()
    {
        Debug.Log($"This active scene Name is: {SceneManager.GetActiveScene().name}");
    }
}
