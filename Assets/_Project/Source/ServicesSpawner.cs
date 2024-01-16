using UnityEngine;

public class ServicesSpawner : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        SpawnScreenService();
        ISceneService sceneService = ServiceLocator.Instance.GetService<ISceneService>();
        sceneService.LoadingScene(_sceneName);
    }

    private void SpawnScreenService()
    {
        GameObject screenServiceObject = new GameObject(nameof(SceneService));
        DontDestroyOnLoad(screenServiceObject);
        SceneService sceneService = new SceneService();
        ServiceLocator.Instance.RegisterService<ISceneService>(sceneService);
    }
}
