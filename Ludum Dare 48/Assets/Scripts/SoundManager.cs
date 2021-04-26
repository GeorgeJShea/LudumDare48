using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject SoundSourcePrefab;

    public int Amount = 1000;

    private List<AudioSource> SourcesGenerated = new List<AudioSource>();

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < Amount; i++)
        {
            AudioSource source = Instantiate(SoundSourcePrefab, transform).GetComponent<AudioSource>();
            SourcesGenerated.Add(source);
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume, float pitch = 1)
    {
        AudioSource source = GetSource();

        source.transform.position = position;
        source.pitch = pitch;
        source.PlayOneShot(clip, volume);
    }

    public AudioSource GetSource()
    {
        AudioSource source = SourcesGenerated[0];

        SourcesGenerated.RemoveAt(0);
        SourcesGenerated.Add(source);

        return source;
    }
}
