using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelController))]
public class TrackCarAi : MonoBehaviour
{
    public WaypointsContainer waypointsContainer;   // Container ที่เก็บจุด waypoint
    public List<Transform> waypoints;              // รายชื่อ waypoint จริง ๆ ที่ใช้
    public int currentWaypoint;                    // index ของ waypoint ปัจจุบัน

    private WheelController wheelController;
    public float waypointRange = 5f;                // รัศมีระยะที่รถจะเปลี่ยนไปยัง waypoint ถัดไป

    private float currentAngle;
    public float maximumAngle = 45f;                // มุมเลี้ยวสูงสุดที่รถสามารถทำได้
    public float maximumSpeed = 120f;               // ยังไม่ได้ใช้ตอนนี้ (option สำหรับจำกัดความเร็ว)

    [Range(0.01f, 0.04f)]
    public float turningConstant = 0.02f;           // ยังไม่ใช้ในโค้ด (option สำหรับเลี้ยวโค้ง)
    private float stuckTimer = 0f;
    private float reverseTimer = 0f;
    private bool isReversing = false;
    void Start()
    {
        wheelController = GetComponent<WheelController>();

        // ตั้งให้ควบคุมด้วย script ไม่ใช่ keyboard
        wheelController.control = WheelController.ControlMode.Buttons;

        // ตรวจสอบว่า WaypointsContainer มีค่า
        if (waypointsContainer != null && waypointsContainer.waypoints.Count > 0)
        {
            waypoints = waypointsContainer.waypoints;
        }
        else
        {
            Debug.LogError("Waypoint container is not assigned or empty!");
        }

        currentWaypoint = 0;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        // ถ้ารถเข้าใกล้ waypoint มากพอ ให้ขยับไปยัง waypoint ถัดไป
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < waypointRange)
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count)
                currentWaypoint = 0;
        }
        float currentSpeed = wheelController.GetComponent<Rigidbody>().linearVelocity.magnitude;
        if (!isReversing)
        {
            if (currentSpeed < 0.1f)
                stuckTimer += Time.deltaTime;
            else
                stuckTimer = 0f;

            if (stuckTimer > 2f)
            {
                isReversing = true;
                reverseTimer = 0f;
            }
        }

        if (isReversing)
        {
            reverseTimer += Time.deltaTime;
            wheelController.MoveInput(-0.5f);
            wheelController.SteerInput(0);

            if (reverseTimer > 3f)
            {
                isReversing = false;
                stuckTimer = 0f;
            }
        }
        else
        {
            // คำนวณทิศทางที่ต้องเลี้ยวไป
            Vector3 directionToWaypoint = waypoints[currentWaypoint].position - transform.position;
            directionToWaypoint.y = 0; // ละทิ้งแกน Y เพื่อไม่ให้รถกระโดดหรือคำนวณพลาด
            currentAngle = Vector3.SignedAngle(transform.forward, directionToWaypoint, Vector3.up);

            float steerInput = Mathf.Clamp(currentAngle / maximumAngle, -1f, 1f); // normalize ให้ระหว่าง -1 ถึง 1

            // ส่งค่าไปที่ WheelController
            wheelController.MoveInput(1f); // วิ่งเต็มกำลัง
            wheelController.SteerInput(steerInput);
        }

        // วาดเส้น Debug ไปยัง waypoint ปัจจุบัน
        Debug.DrawLine(transform.position + Vector3.up * 1f, waypoints[currentWaypoint].position, Color.red);
    }
}
