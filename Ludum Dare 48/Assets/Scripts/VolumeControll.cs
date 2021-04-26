using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeControll : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void setSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVol", volume);
    }
}
