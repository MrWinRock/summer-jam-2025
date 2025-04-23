using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCutscene : MonoBehaviour
{
    public int sceneBuildIndex;
    void OnEnable()
    {
        // Unload the previous scene to ensure no assets remain
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // Load the new scene in additive mode
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
    }
}