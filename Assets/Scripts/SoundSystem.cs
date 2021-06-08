using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource musicAi;
    public AudioSource sfxAi;
    private void Start()
    {
        SetSources();
    }
    public void SetSources()  //Kaynak seslerini değerlere atanır.
    {
        musicAi.volume = PlayerData.musicVolume;
        sfxAi.volume = PlayerData.sfxVolume;
    }
    public void PlaySfx(AudioClip clip)  //Sfx oynatır
    {
        sfxAi.PlayOneShot(clip,1);
    }
}
