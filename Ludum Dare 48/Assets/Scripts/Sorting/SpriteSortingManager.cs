using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Burst;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;
using System.Threading.Tasks;

public class SpriteSortingManager : MonoBehaviour
{
    public List<SpriteSorting> Sprites = new List<SpriteSorting>();

    public static SpriteSortingManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Sprites.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < Sprites.Count; i++)
        {
            foreach (var r in Sprites[i].renderers)
            {
                if (r)
                {
                    r.sortingOrder = i;
                }
            }
        }

        Sprites.Sort((s1, s2) => s2.SortingPoint.y.CompareTo(s1.SortingPoint.y));
    }

    public void UpdateSorting()
    {
        Sprites.Sort((s1, s2) => s2.SortingPoint.y.CompareTo(s1.SortingPoint.y));

        for (int i = 0; i < Sprites.Count; i++)
        {
            foreach (var r in Sprites[i].renderers)
            {
                r.sortingOrder = i;
            }
        }
    }
}