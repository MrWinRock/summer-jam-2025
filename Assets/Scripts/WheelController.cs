using UnityEngine;
using System;
using System.Collections.Generic;

public class WheelController : MonoBehaviour
{
    float GetHorizontalSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(carRb.velocity.x, 0, carRb.velocity.z);
        return horizontalVelocity.magnitude;
    }

    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    public float _steerAngle;
    public float boosterForce = 1000f;
    public float maxSpeed = 5f; // Maximum speed in Unity units per second

    public AudioSource crashSound;
    public AudioSource driftSound;
    public AudioSource nitroSound;
    
    public GameObject nitroEffect1;
    public GameObject nitroEffect2;
    public GameObject nitroEffect3;
    public GameObject nitroEffect4;
    
    public float nitroDuration = 1f;
    
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
        nitroEffect1.SetActive(false);
        nitroEffect2.SetActive(false);
        nitroEffect3.SetActive(false);
        nitroEffect4.SetActive(false);
        
        driftSound.Stop();
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();
        Debug.Log(boosterForce);
        
    }

    void LateUpdate()
    {
        Move();
        Steer();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        float currentSpeed = GetHorizontalSpeed();

        
        foreach (var wheel in wheels)
        {
            if (currentSpeed < maxSpeed || boosterForce > 800f) // allow overspeeding when boosting
            {
                wheel.wheelCollider.motorTorque = moveInput * boosterForce * maxAcceleration;
            }
            else
            {
                wheel.wheelCollider.motorTorque = 0f; // stop applying torque when over speed
            }
        }
    }


    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                Debug.Log(_steerAngle);
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);


            }
        }
    }
    
    
    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            //var dirtParticleMainSettings = wheel.smokeParticle.main;
            if ((_steerAngle < -20 || _steerAngle > 20) && wheel.wheelCollider.isGrounded)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                nitroEffect1.SetActive(true);
                nitroEffect2.SetActive(true);
                if (driftSound.isPlaying == false)
                    driftSound.Play();
            }
            else
            {
                Debug.Log("not drifting");
                driftSound.Stop();
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
                nitroEffect1.SetActive(false);
                nitroEffect2.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            nitroEffect1.SetActive(true);
            nitroEffect2.SetActive(true);
            nitroEffect3.SetActive(true);
            nitroEffect4.SetActive(true);
            nitroSound.Play();
            Debug.Log("Boost");
            boosterForce = 3000;
            Invoke(nameof(ResetBosterForce), nitroDuration);
        }
    }

    private void ResetBosterForce()
    {
        nitroEffect1.SetActive(false);
        nitroEffect2.SetActive(false);
        nitroEffect3.SetActive(false);
        nitroEffect4.SetActive(false);
        boosterForce = 800;
    }

    private void OnCollisionEnter(Collision other)
    {
        crashSound.Play();
        
    }
}
