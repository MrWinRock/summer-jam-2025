using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCutscene : MonoBehaviour
{
    public int sceneBuildIndex;
    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
    }
}