using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter instance;
    public int deathCount = 0;

    [SerializeField] private int notResetSceneIndex2 = 5; // <== Change this to your reset scene name
    private int notResetSceneIndex1 = 6; // <== Change this to your not reset scene name
    private int originalSceneIndex;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            originalSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Only reset if we go to a specific scene (e.g. "MainMenu")
        if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            Debug.Log("Not Resetting Death Count");
        }
        else
        {
            deathCount = 0; // Reset the death count
            Destroy(gameObject); // this will reset everything next time the original scene is entered
        }
    }
}
