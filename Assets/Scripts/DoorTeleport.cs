using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject hintUI;

    private bool playerInZone = false;
    private GameObject player;

    private void Start()
    {
        if (hintUI != null)
            hintUI.SetActive(false);
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            player.transform.position = teleportPoint.position;

            if (hintUI != null)
                hintUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            player = other.gameObject;

            if (hintUI != null)
                hintUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            player = null;

            if (hintUI != null)
                hintUI.SetActive(false);
        }
    }
}