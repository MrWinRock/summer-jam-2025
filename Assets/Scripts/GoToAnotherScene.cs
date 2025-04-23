using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToAnotherScene : MonoBehaviour
{
    public float delay = 0f; // Delay in seconds before loading the next scene
    private bool isLoaded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (!isLoaded && delay <= 0f)
        {
            isLoaded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
