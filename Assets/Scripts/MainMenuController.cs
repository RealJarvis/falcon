using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private Image fadeOverlay;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float musicFadeDuration = 1.5f;

    private bool isStarting = false;

    private void Start()
    {
        if (fadeOverlay != null)
        {
            Color c = fadeOverlay.color;
            c.a = 0f;
            fadeOverlay.color = c;
        }
    }

    public void StartGame()
    {
        Debug.Log("BUTTON WORKS");
        if (isStarting) return;
        isStarting = true;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        float timer = 0f;

        float startVolume = 1f;
        if (menuMusic != null)
            startVolume = menuMusic.volume;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            if (fadeOverlay != null)
            {
                Color c = fadeOverlay.color;
                c.a = Mathf.Lerp(0f, 1f, t);
                fadeOverlay.color = c;
            }

            if (menuMusic != null)
            {
                float musicT = Mathf.Clamp01(timer / musicFadeDuration);
                menuMusic.volume = Mathf.Lerp(startVolume, 0f, musicT);
            }

            yield return null;
        }

        SceneManager.LoadScene(gameSceneName);
    }
}
