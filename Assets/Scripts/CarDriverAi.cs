using UnityEngine;

[RequireComponent(typeof(WheelController))]
public class CarDriverAI : MonoBehaviour
{
    public Transform playerCar;
    public float followDistance = 5f;
    public float maxFollowSpeed = 10f;
    public float turnSpeed = 2f;
    public float flipTime = 5f;

    private WheelController controller;

    private float stuckTimer = 0f;
    private float reverseTimer = 0f;

    Vector3 lastPosition;
    private bool isReversing = false;

    void Start()
    {
        controller = GetComponent<WheelController>();
        controller.control = WheelController.ControlMode.Buttons;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (playerCar == null) return;

        Vector3 dirToPlayer = playerCar.position - transform.position;
        Vector3 flatDir = new Vector3(dirToPlayer.x, 0, dirToPlayer.z);
        float distance = flatDir.magnitude;

        Vector3 localDir = transform.InverseTransformDirection(flatDir.normalized);
        float steer = Mathf.Clamp(localDir.x, -1f, 1f) * turnSpeed;
        controller.SteerInput(steer);

        float currentSpeed = controller.GetComponent<Rigidbody>().linearVelocity.magnitude;

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
            controller.MoveInput(-0.5f);
            controller.SteerInput(0);

            if (reverseTimer > 3f)
            {
                isReversing = false;
                stuckTimer = 0f;
            }
        }
        else
        {
            float speedFactor = Mathf.Clamp01((distance - followDistance) / 10f);
            float throttle = speedFactor; // Increase throttle when far, decrease when close
            controller.MoveInput(throttle);
        }
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
            if ((transform.rotation.eulerAngles.z > 150f && transform.rotation.eulerAngles.z < 210f) || 
                (transform.rotation.eulerAngles.y > 150f && transform.rotation.eulerAngles.y < 210f))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
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
