using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            CheckPointManager.instance?.AddScore();
            Destroy(gameObject); // หรือ SetActive(false) ถ้าไม่อยากให้หายไปเลย
        }
    }
}
