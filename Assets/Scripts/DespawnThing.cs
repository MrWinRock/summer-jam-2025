using System.Collections.Generic;
using UnityEngine;

public class DespawnThing : MonoBehaviour
{
    public List<GameObject> things = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject a in things)
            {
                a.SetActive(false);
            }
        }
    }
}
