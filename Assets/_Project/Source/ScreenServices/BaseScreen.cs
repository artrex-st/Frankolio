using DataService;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] protected ScreenReference _thisScreenRef;
    protected ISaveDataService SaveDataService;
    protected IScreenService ScreenService;
    protected ISoundService SoundService;

    protected void Initialize()
    {
        SaveDataService = ServiceLocator.Instance.GetService<ISaveDataService>();
        ScreenService = ServiceLocator.Instance.GetService<IScreenService>();
        SoundService = ServiceLocator.Instance.GetService<ISoundService>();
#if UNITY_EDITOR
        Debug.Log($"Initialize <color=white>{_thisScreenRef.SceneName}</color>");
#endif
    }

    protected void Dispose()
    {
#if UNITY_EDITOR
        Debug.Log($"Disposed <color=Green>{_thisScreenRef.SceneName}</color>");
#endif
    }
}
