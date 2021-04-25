using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform SpawnPoint;

    public List<LevelData> Levels = new List<LevelData>();

    public int currentLevel = 1;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ILoadNextLevel());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ILoadNextLevel());
        }
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

        if (Levels.Count > 1)
        {
            LevelData lastLevel = Levels[Levels.Count - 2];

            data.LevelContainer.position = (lastLevel.ExitPos.position - data.EntrancePos.position) + new Vector3(1, 0, 0);

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
