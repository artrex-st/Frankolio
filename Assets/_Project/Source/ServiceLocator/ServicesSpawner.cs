using DataService;
using UnityEngine;
using UnityEngine.Audio;

public class ServicesSpawner : MonoBehaviour
{
    [SerializeField] private ScreenReference _sceneName;
    [Header("Save Data Configuration")]
    [SerializeField] private string _saveDataName = "data.json";
    [SerializeField] private bool _useEncryption = false;
    [Header("Sound Configuration")]
    [SerializeField] private SoundLibrary _library;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _masterMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private AudioMixerGroup _sfxMixerGroup;
    [SerializeField] private AudioMixerGroup _uiSfxMixerGroup;

    private void Awake()
    {
        SpawnEventService();
        SpawnSaveDataService();
        SpawnSoundService();
        SpawnScreenService();
    }

    private void SpawnScreenService()
    {
        GameObject screenServiceObject = new GameObject(nameof(SceneService));
        DontDestroyOnLoad(screenServiceObject);

        SceneService sceneService = screenServiceObject.AddComponent<SceneService>();
        ServiceLocator.Instance.RegisterService<ISceneService>(sceneService);

        sceneService.LoadingScene(_sceneName);
    }

    private void SpawnSaveDataService()
    {
        GameObject saveDataServiceObject = new GameObject(nameof(SaveDataService));
        DontDestroyOnLoad(saveDataServiceObject);

        SaveDataService saveDataService = saveDataServiceObject.AddComponent<SaveDataService>();
#if !UNITY_EDITOR
        _useEncryption = true;
#endif
        saveDataService.Initialize(_saveDataName, _useEncryption);
        ServiceLocator.Instance.RegisterService<ISaveDataService>(saveDataService);
    }

    private void SpawnSoundService()
    {
        GameObject soundServiceObject = new GameObject(nameof(SoundService));
        DontDestroyOnLoad(soundServiceObject);

        SoundService soundService = soundServiceObject.AddComponent<SoundService>();
        soundService.Initialize(_library, _audioMixer, _musicMixerGroup, _sfxMixerGroup, _uiSfxMixerGroup);
        ServiceLocator.Instance.RegisterService<ISoundService>(soundService);
    }

    private void SpawnEventService()
    {
        GameObject eventServiceObject = new GameObject(nameof(EventsService));
        DontDestroyOnLoad(eventServiceObject);

        EventsService eventsService = eventServiceObject.AddComponent<EventsService>();
        ServiceLocator.Instance.RegisterService<IEventsService>(eventsService);
    }
}
