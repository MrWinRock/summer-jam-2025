using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter instance;
    public int deathCount = 0;

    [SerializeField] private string resetSceneName = "Mainmenu"; // <== Change this to your reset scene name
    private string originalScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            originalScene = SceneManager.GetActiveScene().name;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Only reset if we go to a specific scene (e.g. "MainMenu")
        if (SceneManager.GetActiveScene().name == resetSceneName)
        {
            Destroy(gameObject); // this will reset everything next time the original scene is entered
        }
    }
}
