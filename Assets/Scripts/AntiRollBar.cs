using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AntiRollBar : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public float AntiRoll = 5000.0f;
    public float flipTime = 5f;
        
    private Rigidbody car;
    private Transform carTransform;

    void Start()
    {
        car = GetComponent<Rigidbody>();
        carTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;


        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) /
                      WheelL.suspensionDistance;
        }

        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) /
                      WheelR.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
            car.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);

        if (groundedR)
            car.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with obstacle detected!");
            // Handle the collision with the obstacle here
            // For example, you can apply a force to the car or change its state
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            flipTime -= Time.deltaTime;
            Debug.Log("Collision with ground detected!");
            if (transform.rotation.eulerAngles.z > 150f || transform.rotation.eulerAngles.z < -150f || transform.rotation.eulerAngles.y > 150f || transform.rotation.eulerAngles.y < -150f && flipTime < 3f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.y, 0);
                flipTime = 5f;
            }
        }
        else
        {
            Debug.Log("Collision with unknown object detected!");
            // Handle other collisions here
        }
    }
}