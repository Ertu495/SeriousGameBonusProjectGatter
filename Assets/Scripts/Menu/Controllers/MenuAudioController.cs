using UnityEngine;
using UnityEngine.UI;

public class MenuAudioController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource rightSoundSource;
    [SerializeField] private AudioSource wrongSoundSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundVolumeKey = "SoundVolume";
    

    private void Start()
    {
        LoadAudioSettings();
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }

        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float volume)
    {
        if (soundSource != null && rightSoundSource != null && wrongSoundSource != null)
        {
            soundSource.volume = volume;
            rightSoundSource.volume = volume;
            wrongSoundSource.volume = volume;
        }

        PlayerPrefs.SetFloat(SoundVolumeKey, volume);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
        float savedSoundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 0.5f);

        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
        }

        if (soundSlider != null)
        {
            soundSlider.value = savedSoundVolume;
        }

        if (musicSource != null)
        {
            musicSource.volume = savedMusicVolume;
        }

        if (soundSource != null && rightSoundSource != null && wrongSoundSource != null)
        {
            soundSource.volume = savedSoundVolume;
            rightSoundSource.volume = savedSoundVolume;
            wrongSoundSource.volume = savedSoundVolume;
        }
    }
}