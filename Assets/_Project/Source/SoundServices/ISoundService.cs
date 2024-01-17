using UnityEngine;
using UnityEngine.Audio;

public interface ISoundService
{
    public SoundLibrary SoundLibrary { get; }
    public float MasterVolume { get; set; }
    public float MusicVolume { get; set; }
    public float SfxVolume { get; set; }
    public float UiSfxVolume { get; set; }

    public void Initialize(SoundLibrary library, AudioMixer mixerAudio, AudioMixerGroup musicGroup, AudioMixerGroup sfxGroup, AudioMixerGroup uiSfxGroup);
    public void PlayMusic(AudioClip clip);
    public void PlayUiSfx(AudioClip clip);

}
