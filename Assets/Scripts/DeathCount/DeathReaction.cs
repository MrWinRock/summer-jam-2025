using UnityEngine;

public class DeathReaction : MonoBehaviour
{
    public AudioSource death3Times;
    public AudioSource death5Times;
    public AudioSource death7Times;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int deathCount = DeathCounter.deathCounter;
        
        if (deathCount == 3)
        {
            death3Times.Play();
        }
        else if (deathCount == 5)
        {
            death5Times.Play();
        }
        else if(deathCount == 8)
        {
            death7Times.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
