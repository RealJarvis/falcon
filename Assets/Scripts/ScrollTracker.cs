using UnityEngine;
using UnityEngine.UI;

public class ScrollTracker : MonoBehaviour
{
    public static ScrollTracker Instance;
    public ScrollRect feedScrollRect;
    public int totalPostsScrolled = 0;
    private float lastScrollPos = 1f;
    private float scrollCooldown = 0f;

    void Awake()
    {
        Instance = this;
    }



    void Update()
    {
        if (feedScrollRect == null) return;

        scrollCooldown -= Time.deltaTime;

        float currentPos = feedScrollRect.verticalNormalizedPosition;

        if (currentPos < lastScrollPos - 0.001f && scrollCooldown <= 0f)
        {
            totalPostsScrolled++;
            lastScrollPos = currentPos;
            scrollCooldown = 0.2f;
            CheckUnlocks();
        }
    }

    void CheckUnlocks()
    {
        InteractableObject[] allA = FindObjectsOfType<InteractableObject>();
        foreach (InteractableObject obj in allA)
        {
            if (totalPostsScrolled >= obj.unlockAtScroll)
                obj.SetUnlocked(true);
        }

        Interactable[] allB = FindObjectsOfType<Interactable>();
        foreach (Interactable obj in allB)
        {
            if (totalPostsScrolled >= obj.unlockAtScroll)
                obj.SetUnlocked(true);
        }
    }

    public void ResetScroll()
    {
        totalPostsScrolled = 0;
        lastScrollPos = 1f;
        if (feedScrollRect != null)
            feedScrollRect.verticalNormalizedPosition = 1f;
    }
}