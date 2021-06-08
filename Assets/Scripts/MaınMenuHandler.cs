using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Butonlara basınca çalışacak fonksiyonlar ve açılacak paneller vs. Seslerde dahil!
public class MaınMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject helpPanel;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private AudioSource musicAi;
    [SerializeField] private AudioSource sfxAi;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        LoadSounds();
        musicSlider.value = PlayerData.musicVolume;
        sfxSlider.value = PlayerData.sfxVolume;

        musicAi.volume = PlayerData.musicVolume;
        sfxAi.volume = PlayerData.sfxVolume;
    }

    public void StartGame()
    {
        sfxAi.PlayOneShot(clickSound, 1);
        SceneManager.LoadScene("GameScene");
    }
    public void Settings()
    {
        sfxAi.PlayOneShot(clickSound,1);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        sfxAi.PlayOneShot(clickSound, 1);
        PlayerData.musicVolume = musicSlider.value;
        PlayerData.sfxVolume = sfxSlider.value;
        musicAi.volume = PlayerData.musicVolume;
        sfxAi.volume = PlayerData.sfxVolume;
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
        helpPanel.SetActive(false);
    }
    public void HelpPanel()
    {
        sfxAi.PlayOneShot(clickSound, 1);
        menuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }
    public void QuitGame()
    {
        sfxAi.PlayOneShot(clickSound, 1);
        SaveSounds();
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        SaveSounds();
    }
    private void SaveSounds()
    {
        PlayerPrefs.SetFloat("music",PlayerData.musicVolume);
        PlayerPrefs.SetFloat("sfx", PlayerData.sfxVolume);
    }
    private void LoadSounds()
    {
        PlayerData.musicVolume = PlayerPrefs.GetFloat("music");
        PlayerData.sfxVolume = PlayerPrefs.GetFloat("sfx");
    }
}
