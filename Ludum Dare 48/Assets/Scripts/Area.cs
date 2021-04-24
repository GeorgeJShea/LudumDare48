using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [HideInInspector]public Transform[] Waypoints;

    [HideInInspector]public List<Transform> FreeWaypoints = new List<Transform>();

    private void Awake()
    {
        Waypoints = GetComponentsInChildren<Transform>();
    }

    public Transform GetWaypoint()
    {
        if (Waypoints.Length == 0) return null;

        if (FreeWaypoints.Count <= 0)
        {
            FreeWaypoints = new List<Transform>(Waypoints);
        }

        Transform waypoint = FreeWaypoints[Random.Range(0, FreeWaypoints.Count)];
        FreeWaypoints.Remove(waypoint);
        return waypoint;
    }
}
