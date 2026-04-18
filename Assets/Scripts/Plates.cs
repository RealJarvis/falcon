using UnityEngine;

public class Plates : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
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

