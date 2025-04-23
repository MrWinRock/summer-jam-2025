using UnityEngine;
using System.Collections.Generic;

public class SpawnThing : MonoBehaviour
{
    public List<GameObject> things = new List<GameObject>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject t in things)
        {
            t.SetActive(true);
        }
    }
    

}
