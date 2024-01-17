using DataService;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private ScreenReference _thisScene;
        [SerializeField] private Button _closeButton;
        [Header("Settings UiOverlay elements")]
        [SerializeField] private Slider _sliderMaster;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSfx;
        [SerializeField] private Slider _sliderUiSfx;

        protected ISaveDataService SaveDataService;
        protected ISceneService SceneService;
        protected ISoundService SoundService;

        private void Awake()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Initialize()
        {
            SaveDataService = ServiceLocator.Instance.GetService<ISaveDataService>();
            SceneService = ServiceLocator.Instance.GetService<ISceneService>();
            SoundService = ServiceLocator.Instance.GetService<ISoundService>();

            _closeButton.onClick.AddListener(CloseButtonClickHandler);
            _sliderMaster.onValueChanged.AddListener(OnMasterVolumeChangeHandler);
            _sliderMusic.onValueChanged.AddListener(OnMusicVolumeChangeHandler);
            _sliderSfx.onValueChanged.AddListener(OnSfxVolumeChangeHandler);
            _sliderUiSfx.onValueChanged.AddListener(OnUiSfxVolumeChangeHandler);

            SyncSlidersFromMixers();
        }

        private void OnMasterVolumeChangeHandler(float value)
        {
            SoundService.MasterVolume = value;
        }

        private void OnMusicVolumeChangeHandler(float value)
        {
            SoundService.MusicVolume = value;
        }

        private void OnSfxVolumeChangeHandler(float value)
        {
            SoundService.SfxVolume = value;
        }

        private void OnUiSfxVolumeChangeHandler(float value)
        {
            SoundService.UiSfxVolume = value;
        }

        private void CloseButtonClickHandler()
        {
            SaveSoundVolume();
            SceneService.UnLoadAdditiveSceneAsync(_thisScene);
        }

        private void SyncSlidersFromMixers()
        {
            _sliderMaster.value = SaveDataService.GameData.MasterVolume;
            _sliderMusic.value = SaveDataService.GameData.MusicVolume;
            _sliderSfx.value = SaveDataService.GameData.SfxVolume;
            _sliderUiSfx.value = SaveDataService.GameData.UiSfxVolume;
        }

        private void SaveSoundVolume()
        {
            SaveDataService.GameData.MasterVolume = SoundService.MasterVolume;
            SaveDataService.GameData.MusicVolume = SoundService.MusicVolume;
            SaveDataService.GameData.SfxVolume = SoundService.SfxVolume;
            SaveDataService.GameData.UiSfxVolume = SoundService.UiSfxVolume;
            SaveDataService.SaveGame();
        }

        private void Dispose()
        {
            _closeButton.onClick.RemoveAllListeners();
            _sliderMaster.onValueChanged.RemoveAllListeners();
            _sliderMusic.onValueChanged.RemoveAllListeners();
            _sliderSfx.onValueChanged.RemoveAllListeners();
            _sliderUiSfx.onValueChanged.RemoveAllListeners();
        }
    }
}
