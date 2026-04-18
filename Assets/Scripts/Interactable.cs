using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject textBoxUI;
    public TMP_Text interactionText;
    public bool isBed = false;
    public int unlockAtScroll = 5;
    private bool isUnlocked = false;

    [TextArea(2, 5)]
    public string[] messages = {
        "Night 1 text.",
        "Night 2 text.",
        "Night 3 text.",
        "Night 4 text."
    };

    private bool playerNearby = false;
    private bool textOpen = false;

    void Start()
    {
        promptUI.SetActive(false);
        textBoxUI.SetActive(false);
    }

    void Update()
    {
        if (!isUnlocked) return;

        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            textOpen = !textOpen;
            if (textOpen)
            {
                int night = NightManager.Instance != null ? NightManager.Instance.currentNight - 1 : 0;
                night = Mathf.Clamp(night, 0, messages.Length - 1);
                interactionText.text = messages[night];
                textBoxUI.SetActive(true);
                promptUI.SetActive(false);

                if (isBed)
                    NightManager.Instance.GoToSleep();
            }
            else
            {
                textBoxUI.SetActive(false);
                promptUI.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (!textOpen && isUnlocked)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            textOpen = false;
            promptUI.SetActive(false);
            textBoxUI.SetActive(false);
        }
    }

    public void SetUnlocked(bool state)
    {
        isUnlocked = state;
    }

    public void ForceClose()
    {
        textOpen = false;
        playerNearby = false;
        if (promptUI != null) promptUI.SetActive(false);
        if (textBoxUI != null) textBoxUI.SetActive(false);
    }
}