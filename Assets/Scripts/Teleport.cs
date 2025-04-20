using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] public GameObject teleportTarget;
    [SerializeField] public GameObject cameraTarget;
    
    private Transform carTransform;
    private Transform cameraTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carTransform = GetComponent<Transform>();
        carTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            carTransform.position = teleportTarget.transform.position;
            cameraTransform.position = cameraTarget.transform.position;
        }
    }
}
