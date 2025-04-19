using System;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider fR;
    [SerializeField] private WheelCollider fL;
    [SerializeField] private WheelCollider rR;
    [SerializeField] private WheelCollider rL;
    
    [SerializeField] private Transform fRTransform;
    [SerializeField] private Transform fLTransform;
    [SerializeField] private Transform rRTransform;
    [SerializeField] private Transform rLTransform;
    
    public float acceleration = 2000f;
    public float maxTurnAngle = 30f;
    
    private float currentAcceleration = 0f;
    private float currentTurnAngle = 0f;
    
    private void FixedUpdate()
    {
        // forward and backward movement W/S
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        
        
        fR.motorTorque = currentAcceleration;
        fL.motorTorque = currentAcceleration;
        
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        fR.steerAngle = currentTurnAngle;
        fL.steerAngle = currentTurnAngle;
        
        UpdateWheel(fL, fLTransform);
        UpdateWheel(fR, fRTransform);
        UpdateWheel(rL, rLTransform);
        UpdateWheel(rR, rRTransform);
        
        Console.WriteLine(currentAcceleration);
        
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);

        trans.position = pos;
        trans.rotation = rot;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
