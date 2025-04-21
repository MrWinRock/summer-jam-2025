using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrel : MonoBehaviour
{
    public List<GameObject> barrel = new List<GameObject>();
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
            foreach (GameObject b in barrel)
            {
                b.SetActive(true);
            }
        }
    }
}
