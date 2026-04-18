using UnityEngine;

public class TV : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] tvChannels;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool playerInRange;
    private int currentChannel = 0;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (tvChannels != null && tvChannels.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = tvChannels[currentChannel];
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            ChangeChannel();
        }
    }

    private void ChangeChannel()
    {
        if (tvChannels == null || tvChannels.Length == 0 || spriteRenderer == null)
            return;

        currentChannel++;
        if (currentChannel >= tvChannels.Length)
            currentChannel = 0;

        spriteRenderer.sprite = tvChannels[currentChannel];

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.Play();
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