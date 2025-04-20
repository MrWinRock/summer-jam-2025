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



    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
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
            if (currentSpeed < maxSpeed || boosterForce > 1000f) // allow overspeeding when boosting
            {
                wheel.wheelCollider.motorTorque = moveInput * boosterForce * maxAcceleration * Time.deltaTime;
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
            if ((_steerAngle < -20 || _steerAngle > 20) && wheel.wheelCollider.isGrounded == true)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            Debug.Log("Boost");
            boosterForce = 100000f;
            Invoke(nameof(ResetBosterForce), 3f);
        }
    }

    private void ResetBosterForce()
    {
        boosterForce = 1000f;
    }
}
