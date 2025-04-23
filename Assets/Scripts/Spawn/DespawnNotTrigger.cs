using UnityEngine;
using System.Collections.Generic;

public class DespawnNotTrigger : MonoBehaviour
{
    public List<GameObject> things = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject t in things)
        {
            t.SetActive(false);
        }
    }
}
