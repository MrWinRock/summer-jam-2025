using UnityEngine;

public class PoliceLight : MonoBehaviour
{
    public Light redLight;
    public Light blueLight;
    public float switchInterval = 0.5f; // seconds

    private float timer;

    void Start()
    {
        timer = 0f;
        redLight.enabled = true;
        blueLight.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            // Toggle lights
            redLight.enabled = !redLight.enabled;
            blueLight.enabled = !blueLight.enabled;

            timer = 0f;
        }
    }
}
