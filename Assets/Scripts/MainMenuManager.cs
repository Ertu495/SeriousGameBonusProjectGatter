using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public GameObject MainMenuUI;
    public GameObject PlayMenuUI;
    public GameObject PlayGameMenuUI;
    public GameObject SelectLevelMenuUI;
    public GameObject SettingsMenuUI;
    public GameObject CreditsMenuUI;
    public GameObject BackButtonUI;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public AudioSource musicSource;
    public AudioSource SoundSource;
    

    public void Start(){
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float savedSound = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        if(MusicSlider != null) MusicSlider.value = savedMusic;
        if(SoundSlider != null) SoundSlider.value = savedSound;
        if (musicSource != null) 
        {
            musicSource.volume = savedMusic;
        }
        if (SoundSource != null) 
        {
            SoundSource.volume = savedSound;
        }
        OpenMainMenu();
    }

    public void OpenMainMenu(){
        MainMenuUI.SetActive(true);
        PlayMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(false);
        BackButtonUI.SetActive(false);
    }

    //Selecting the Play Submenu Layers
    public void PlayGame(){
        MainMenuUI.SetActive(false);
        PlayMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(false);
        BackButtonUI.SetActive(true);
    }

    //Selecting the Level
    public void SelectLevel(String scene){
        SceneManager.LoadScene(scene);
    }

    //Closing the Game
    public void ExitGame(){
        Application.Quit();
    }

    //Selecting the layers of the Settings Submenu
    public void OpenSettings(){
        MainMenuUI.SetActive(false);
        PlayMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
        CreditsMenuUI.SetActive(false);
        BackButtonUI.SetActive(true);
    }

    //Selecting the layers of the Credits Submenu
    public void OpenCredits()
    {
        MainMenuUI.SetActive(false);
        PlayMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(true);
        BackButtonUI.SetActive(true);
    }

    public void SetMusicVolume(float volume){
        if (musicSource != null){
            musicSource.volume = volume; 
        }
        PlayerPrefs.SetFloat("Music Volume", volume);
        PlayerPrefs.Save();
    }
    //Sav
    public void SetSoundVolume(float volume){
        if(SoundSource != null){
            SoundSource.volume = volume;
        }
        PlayerPrefs.SetFloat("Sound Volume", volume);
        PlayerPrefs.Save();
    }
}
