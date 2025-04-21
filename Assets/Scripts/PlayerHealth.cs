using UnityEngine;
using UnityEngine.SceneManagement; // ต้องใช้สำหรับโหลดฉากใหม่

public class PlayerHealth : MonoBehaviour
{
    public WheelController wheelController;
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject fireEffect;
    public GameObject expoldeEffect;

    void Start()
    {
        currentHealth = maxHealth;
        fireEffect.SetActive(false);
        expoldeEffect.SetActive(false);
        wheelController = GetComponent<WheelController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
        }
    }

    // ฟังก์ชันรับดาเมจ
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            GetComponent<WheelController>().enabled = false;
            fireEffect.SetActive(true);
            expoldeEffect.SetActive(true);
            Invoke(nameof(Die), 2f);
        }
    }

    void Die()
    {
        Debug.Log("Player died. Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}