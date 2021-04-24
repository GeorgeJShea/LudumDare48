using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeControll : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
