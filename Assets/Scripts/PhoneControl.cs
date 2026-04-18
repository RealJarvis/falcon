using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private RectTransform phoneUI;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private float moveSpeed = 2000f;

    [SerializeField] private Vector2 closedPosition = new Vector2(260f, -400f);
    [SerializeField] private Vector2 openPosition = new Vector2(-300f, -500f);

    private bool isOpen = false;

    private void Start()
    {
        if (phoneUI != null)
            phoneUI.anchoredPosition = closedPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            isOpen = !isOpen;

        if (phoneUI == null) return;

        Vector2 target = isOpen ? openPosition : closedPosition;
        phoneUI.anchoredPosition = Vector2.MoveTowards(
            phoneUI.anchoredPosition,
            target,
            moveSpeed * Time.deltaTime
        );
    }
}