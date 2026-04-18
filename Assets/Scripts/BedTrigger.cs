using UnityEngine;

public class BedTrigger : MonoBehaviour
{
    private bool playerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (NightManager.Instance != null)
                NightManager.Instance.GoToSleep();
        }
    }
}