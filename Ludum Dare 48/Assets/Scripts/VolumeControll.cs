using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeControll : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    public bool isMusic;

    private void Start()
    {
        if (isMusic)
        {
            if(audioMixer.GetFloat("MusicVol", out float vol))
            {
                slider.value = vol;
            }
        }
        else
        {
            if (audioMixer.GetFloat("SFXVol", out float vol))
            {
                slider.value = vol;
            }
        }
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void setSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVol", volume);
    }
}
