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
    public int takePoliceDamage = 20;
    public int takeRockDamage = 35;
    public int takeTrainDamage = 100;
    public int timeUpDamage = 100;
    public int bombDamage = 50;
    
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
        DeathCounter.deathCounter++;
        Debug.Log("Player died. Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Police"))
        {
            TakeDamage(takePoliceDamage);
        }
        if (other.gameObject.CompareTag("Rock"))
        {
            TakeDamage(takeRockDamage);
        }
        if (other.gameObject.CompareTag("Train"))
        {
            TakeDamage(takeTrainDamage);
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