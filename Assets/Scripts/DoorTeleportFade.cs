using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorTeleportFade : MonoBehaviour
{
    public Transform teleportPoint;
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public float waitBeforeTeleport = 0.1f;
    public AudioSource audioSource;
    public AudioClip doorClip;

    private bool playerInZone = false;
    private GameObject player;
    private bool isTransitioning = false;

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F) && !isTransitioning)
        {
            StartCoroutine(TeleportWithFade());
        }
    }

    private IEnumerator TeleportWithFade()
    {
        isTransitioning = true;

        if (audioSource != null && doorClip != null)
            audioSource.PlayOneShot(doorClip);

        yield return StartCoroutine(Fade(0f, 1f));

        yield return new WaitForSeconds(waitBeforeTeleport);

        if (player != null && teleportPoint != null)
            player.transform.position = teleportPoint.position;

        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            player = null;
        }
    }
}