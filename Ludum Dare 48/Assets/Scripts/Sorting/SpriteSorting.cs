using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteSorting : MonoBehaviour
{
    public Vector3 SorterPositionOffset = new Vector3();

    public bool isMovable;

    public SpriteRenderer[] renderers;

    private Transform t;

    private void Reset()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        t = transform;
    }

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        if (SpriteSortingManager.Instance)
        {
            if (!SpriteSortingManager.Instance.Sprites.Contains(this))
            {
                SpriteSortingManager.Instance.Sprites.Add(this);
            }
        }
    }

    public void Setup()
    {
        t = transform;
    }

    public Vector3 SortingPoint
    {
        get
        {
            return SorterPositionOffset + t.position;
        }
    }

#if UNITY_EDITOR
    public void SortScene()
    {
        SpriteSorting[] sorters = FindObjectsOfType(typeof(SpriteSorting)) as SpriteSorting[];
        foreach (var s in sorters)
        {
            s.Setup();
        }
        SpriteSortingManager manager = FindObjectOfType<SpriteSortingManager>();
        if (!manager) return;
        manager.Sprites = sorters.ToList();
        manager.UpdateSorting();
    }
#endif

    private void OnEnable()
    {
        //SpriteSortingManager.Instance.Sprites.Add(this);
    }

    private void OnDisable()
    {
        if (SpriteSortingManager.Instance)
        {
            if (SpriteSortingManager.Instance.Sprites.Contains(this))
            {
                SpriteSortingManager.Instance.Sprites.Remove(this);
            }
        }
    }

    private void OnDestroy()
    {
        if (SpriteSortingManager.Instance)
        {
            if (SpriteSortingManager.Instance.Sprites.Contains(this))
            {
                SpriteSortingManager.Instance.Sprites.Remove(this);
            }
        }
    }
}
