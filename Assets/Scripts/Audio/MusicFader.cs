using UnityEngine;
using System.Collections;

public class MusicFader : MonoBehaviour
{
    public float fadeDuration = 4f;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

// Removed the empty Update method as it was unnecessary.
    IEnumerator FadeIn()
    {
        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 0.5f;
    }
    
}
