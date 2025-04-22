using System;
using UnityEngine;
using UnityEngine.SceneManagement; // ต้องใช้สำหรับโหลดฉากใหม่

public class PlayerHealth : MonoBehaviour
{
    public WheelController wheelController;
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject fireEffect;
    public GameObject expoldeEffect;
    public AudioSource carExplode;
    public AudioSource carFire;

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
            CarExplode();
            Invoke(nameof(Die), 2f);
        }
    }

    void Die()
    {
        Debug.Log("Player died. Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Police"))
        {
            TakeDamage(20);
        }
        if (other.gameObject.CompareTag("Rock"))
        {
            TakeDamage(35);
        }
        if (other.gameObject.CompareTag("Train"))
        {
            TakeDamage(100);
        }
    }

    private void CarExplode()
    {
        GetComponent<WheelController>().enabled = false;
        fireEffect.SetActive(true);
        expoldeEffect.SetActive(true);
        carExplode.Play();
        carFire.Play();
    }
    
}