using UnityEngine;

public class SpawnTeleport : MonoBehaviour
{
    public GameObject teleportPrefab;
    void Start()
    {
        teleportPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleportPrefab.SetActive(true);
            Destroy(gameObject);
        }
    }
}
