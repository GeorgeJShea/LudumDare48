using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelData : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform EntrancePos;
    public Transform ExitPos;
    public Transform LevelContainer;

    private void Awake()
    {
        foreach (var n in FindObjectsOfType<NavMeshAgent>())
        {
            n.gameObject.SetActive(false);
        }

        LevelManager.instance.AddNewLevelData(this);
    }
}
