using DataService;
using UnityEngine;
using UnityEngine.Audio;

public class ServicesSpawner : MonoBehaviour
{
    [Header("Screen Services")]
    [SerializeField] private ScreenReference _sceneName;
    [SerializeField] private ScreenReference _loadingScreenRef;
    [Header("Save Data Services")]
    [SerializeField] private string _saveDataName = "data.json";
    [SerializeField] private bool _useEncryption = false;
    [Header("Sound Services")]
    [SerializeField] private SoundLibrary _library;
    [SerializeField] private AudioMixer _audioMixer;
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
        GameObject screenServiceObject = new GameObject(nameof(ScreenService));
        DontDestroyOnLoad(screenServiceObject);

        ScreenService screenService = screenServiceObject.AddComponent<ScreenService>();
        ServiceLocator.Instance.RegisterService<IScreenService>(screenService);
        screenService.Initialize(_loadingScreenRef);
        screenService.LoadingScene(_sceneName);
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
