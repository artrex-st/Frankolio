using DataService;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class ServicesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _eventSystem;
    [Header("Screen Services")]
    [SerializeField] private ScreenReference _startupScreenName;
    [SerializeField] private ScreenReference _firstSceneName;
    [SerializeField] private ScreenReference _loadingScreenRef;
    [SerializeField] private float _fakeLoadingTime = 0f;
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
        DontDestroyOnLoad(_eventSystem);
        SpawnEventService();
        SpawnSaveDataService();
        SpawnSoundService();
        SpawnGameDataService();
        SpawnScreenService();
    }

    private void SpawnEventService()
    {
        GameObject eventServiceObject = new GameObject(nameof(EventsService));
        DontDestroyOnLoad(eventServiceObject);

        EventsService eventsService = eventServiceObject.AddComponent<EventsService>();
        ServiceLocator.Instance.RegisterService<IEventsService>(eventsService);
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

    private void SpawnGameDataService()
    {
        GameObject gameDataServiceObject = new GameObject(nameof(GameDataService));
        DontDestroyOnLoad(gameDataServiceObject);

        GameDataService gameDataService = gameDataServiceObject.AddComponent<GameDataService>();
        ServiceLocator.Instance.RegisterService<IGameDataService>(gameDataService);

        gameDataService.Initialize();
    }

    private void SpawnScreenService()
    {
        GameObject screenServiceObject = new GameObject(nameof(ScreenService));
        DontDestroyOnLoad(screenServiceObject);

        ScreenService screenService = screenServiceObject.AddComponent<ScreenService>();
        ServiceLocator.Instance.RegisterService<IScreenService>(screenService);
        screenService.Initialize(_startupScreenName, _loadingScreenRef, _fakeLoadingTime);
        screenService.LoadingSceneAsync(_firstSceneName);
    }
}
