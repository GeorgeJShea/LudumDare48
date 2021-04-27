using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform SpawnPoint;

    public List<LevelData> Levels = new List<LevelData>();

    public int currentLevel = 1;

    public AudioSource MusicSource;
    public AudioClip BossfightMusic;
    public AudioClip EndingMusic;

    public AudioMixer Mixer;

    public Image FadeImage;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ILoadNextLevel());
    }

    public void LoadNextLevel()
    {
        StartCoroutine(ILoadNextLevel());
    }

    public void EndGame()
    {
        StartCoroutine(DoEnding());
    }

    public IEnumerator DoEnding()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 0;

        float initialMusicVol = MusicSource.volume;

        float mixerVol;
        Mixer.GetFloat("SFXVol", out mixerVol);

        float timeStamp = Time.unscaledTime;
        float duration = 1f;
        while (Time.unscaledTime < timeStamp + duration)
        {
            float t = (Time.unscaledTime - timeStamp) / duration;

            MusicSource.volume = Mathf.Lerp(initialMusicVol, 0, t);
            Mixer.SetFloat("SFXVol", Mathf.Lerp(mixerVol, 0, t));
            yield return null;
        }

        MusicSource.volume = 0;
        MusicSource.clip = EndingMusic;
        MusicSource.volume = initialMusicVol;
        MusicSource.loop = false;
        MusicSource.Play();

        yield return new WaitForSecondsRealtime(EndingMusic.length);

        Color fadeColor = FadeImage.color;

        timeStamp = Time.unscaledTime;
        duration = 0.2f;
        while (Time.unscaledTime < timeStamp + duration)
        {
            float t = (Time.unscaledTime - timeStamp) / duration;

            fadeColor.a = t;
            FadeImage.color = fadeColor;

            yield return null;
        }

        Mixer.SetFloat("SFXVol", mixerVol);
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public IEnumerator ILoadNextLevel()
    {
        currentLevel++;
        if (currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            var loadScene = SceneManager.LoadSceneAsync(currentLevel, LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadScene.isDone);
        }
    }

    public void AddNewLevelData(LevelData data)
    {
        Levels.Add(data);

        SpawnPoint = data.SpawnPoint;

        if (data.isBossLevel)
        {
            MusicSource.clip = BossfightMusic;
            MusicSource.Play();
        }

        if (Levels.Count > 1)
        {
            LevelData lastLevel = Levels[Levels.Count - 2];

            Vector3 toAdd = (lastLevel.ExitPos.position - data.EntrancePos.position);
            data.LevelContainer.position = toAdd + new Vector3(Mathf.RoundToInt(toAdd.normalized.x), Mathf.RoundToInt(toAdd.normalized.y), 0);

            GameObject temp = new GameObject("navmeshLink");
            temp.transform.position = (lastLevel.ExitPos.position + data.EntrancePos.position) / 2;
            temp.transform.eulerAngles = new Vector3(-90, 0, 0);

            OffMeshLink link = temp.AddComponent<OffMeshLink>();
            link.startTransform = lastLevel.ExitPos;
            link.endTransform = data.EntrancePos;
            link.biDirectional = true;
        }
        foreach (var n in FindObjectsOfType<NavMeshAgent>(true))
        {
            n.gameObject.SetActive(true);
        }

        //data.LevelContainer.position = 
    }
}
