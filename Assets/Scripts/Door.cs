using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;
    private bool isOpen = false;

    private void Update()
    {
        if (playerInRange && !isOpen && Input.GetKeyDown(interactKey))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}