using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public sealed class SettingsMenuController : BaseScreen
    {
        [SerializeField] private Button _closeButton;
        [Header("Settings UiOverlay elements")]
        [SerializeField] private Slider _sliderMaster;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSfx;
        [SerializeField] private Slider _sliderUiSfx;

        private void Awake()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private new void Initialize()
        {
            base.Initialize();

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
            ScreenService.UnLoadAdditiveSceneAsync(_thisScreenRef);
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

        private new void Dispose()
        {
            base.Dispose();
            _closeButton.onClick.RemoveAllListeners();
            _sliderMaster.onValueChanged.RemoveAllListeners();
            _sliderMusic.onValueChanged.RemoveAllListeners();
            _sliderSfx.onValueChanged.RemoveAllListeners();
            _sliderUiSfx.onValueChanged.RemoveAllListeners();
        }
    }
}
