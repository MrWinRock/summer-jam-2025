using UnityEngine;
using System.Collections.Generic;

public class DisableThing : MonoBehaviour
{
    public List<GameObject> things = new List<GameObject>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject t in things)
            {
                t.SetActive(false);
            }
        }
    }
}
