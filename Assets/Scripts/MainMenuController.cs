using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private Image fadeOverlay;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private TMP_Text loadingText;
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

        if (loadingText != null)
        {
            loadingText.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (isStarting) return;
        isStarting = true;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        // if (loadingText != null)
        // {
        //     loadingText.gameObject.SetActive(true);
        // }

        float timer = 0f;
        float startVolume = menuMusic != null ? menuMusic.volume : 1f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);

            if (loadingText != null && t > 0.7f && !loadingText.gameObject.activeSelf)
{
    loadingText.gameObject.SetActive(true);
}

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