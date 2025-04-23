using UnityEngine;

public class AudioScore : MonoBehaviour
{
    public AudioSource fullScoreAudio;
    public AudioSource notFullScoreAudio;
    private CheckPointManager checkPointManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointManager = CheckPointManager.instance;
            if (checkPointManager != null)
            {
                if (checkPointManager.score == 8)
                {
                    fullScoreAudio.Play();
                }
                else
                {
                    notFullScoreAudio.Play();
                }
            }
        }
    }
}
