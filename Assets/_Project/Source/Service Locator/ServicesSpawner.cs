using UnityEngine;

public class ServicesSpawner : MonoBehaviour
{
    [SerializeField] private ScreenReference _sceneName;

    private void Awake()
    {
        SpawnScreenService();
    }

    private void SpawnScreenService()
    {
        GameObject screenServiceObject = new GameObject(nameof(SceneService));
        DontDestroyOnLoad(screenServiceObject);
        SceneService sceneService = new SceneService();
        ServiceLocator.Instance.RegisterService<ISceneService>(sceneService);
        sceneService.LoadingScene(_sceneName);
    }
}
