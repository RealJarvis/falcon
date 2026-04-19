using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class NightManager : MonoBehaviour
{
    public static NightManager Instance;
    public int currentNight = 1;
    public int totalNights = 4;
    public Image fadePanel;
    public TextMeshProUGUI nightText;
    public FeedManager feedManager;
    public int interactionCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void GoToSleep()
    {
        StartCoroutine(NextNight());
    }

    IEnumerator NextNight()
    {
        yield return StartCoroutine(Fade(0, 1, 1.5f));
        currentNight++;

        foreach (Interactable obj in FindObjectsOfType<Interactable>())
        {
            obj.ForceClose();
            obj.SetUnlocked(false);
        }

        foreach (InteractableObject obj in FindObjectsOfType<InteractableObject>())
        {
            obj.ForceClose();
            obj.SetUnlocked(false);
        }

        ScrollTracker.Instance.ResetScroll();

        if (currentNight > totalNights)
        {
            ShowEnding();
            yield break;
        }

        nightText.gameObject.SetActive(true);
        nightText.text = "Night " + currentNight;
        feedManager.NextNight();

        yield return new WaitForSeconds(2f);

        nightText.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(1, 0, 1.5f));
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = fadePanel.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            fadePanel.color = c;
            yield return null;
        }
    }

    public void RegisterInteraction()
    {
        interactionCount++;
    }
    
    void ShowEnding()
    {
        if (interactionCount >= 10)
            SceneManager.LoadScene("GoodEnding");
        else
            SceneManager.LoadScene("BadEnding");
    }
}