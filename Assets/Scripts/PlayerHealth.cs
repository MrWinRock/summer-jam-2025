using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    public List<GameObject> portals = new List<GameObject>();

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
        if (currentHealth <= 0)
        {
            return; // ถ้าสุขภาพเป็น 0 หรือ น้อยกว่า 0 จะไม่ทำอะไร
        }
        currentHealth -= amount;
        Debug.Log("Player took damage. Current health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            CarExplode();
            Invoke(nameof(Die), 2f);
            foreach (GameObject p in portals)
            {
                Destroy(p);
            }
        }
    }

    public void Die()
    {
        if (DeathCounter.instance != null)
        {
            DeathCounter.instance.deathCount++;
            Debug.Log("Death count: " + DeathCounter.instance.deathCount);
            int count = DeathCounter.instance.deathCount;
            
            Debug.Log("Player died. Death count in scene: " + DeathCounter.instance.deathCount);
            if (count == 3)
                FindObjectOfType<DeathReaction>()?.death3Times?.Play();
            else if (count == 5)
                FindObjectOfType<DeathReaction>()?.death5Times?.Play();
            else if (count == 8)
                FindObjectOfType<DeathReaction>()?.death7Times?.Play();
        }
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