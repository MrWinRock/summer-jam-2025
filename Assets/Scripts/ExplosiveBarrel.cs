using System;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject barrel;
    public GameObject explosionEffect;
    private float setActiveTime = 2;
    private bool isExploding = false;

    [SerializeField] private float range;

    public AudioSource bombSound;

    void Awake()
    {
        barrel.SetActive(true);
        explosionEffect.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isExploding)
        {
            Explode();
        }
    }

    void Explode()
    {
        
        bombSound.Play();
        isExploding = true;
        
        explosionEffect.transform.SetParent(null);
        
        explosionEffect.SetActive(true);
        
        Invoke(nameof(DisableBarrel), 0.3f);
        
        Destroy(explosionEffect, 3f);
        
        Collider [] player = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in player)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerHealth>().TakeDamage(50);
            }
        }
    }

    void DisableBarrel()
    {
        barrel.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
