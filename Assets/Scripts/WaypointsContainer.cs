using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsContainer : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            waypoints.Add(tr);
        }

        waypoints.Remove(waypoints[0]);
    }
    
}
