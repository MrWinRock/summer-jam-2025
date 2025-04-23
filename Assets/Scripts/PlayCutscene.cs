using UnityEngine;
using UnityEngine.Playables;

public class PlayCutscene : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;

    private void Start()
    {
        // Ensure the director is stopped at start
        cutsceneDirector.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutsceneDirector.Play();
            Destroy(gameObject); // Prevent multiple triggers
        }
    }
}