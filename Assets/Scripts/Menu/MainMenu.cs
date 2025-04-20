using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingMenu(GameObject target)
    {
        target.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void Back(GameObject target)
    {
        target.SetActive(false);
    }
}
