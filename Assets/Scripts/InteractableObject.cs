using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject textBoxUI;
    public TMP_Text interactionText;

    [TextArea(2, 5)]
    public string message = "Old photo. I have not seen her in months, we were good friends once... should've called her";

    private bool playerNearby = false;
    private bool textOpen = false;

    void Start()
    {
        promptUI.SetActive(false);
        textBoxUI.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            textOpen = !textOpen;

            if (textOpen)
            {
                interactionText.text = message;
                textBoxUI.SetActive(true);
                promptUI.SetActive(false);
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

            if (!textOpen)
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
}