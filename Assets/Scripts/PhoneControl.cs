using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private Animator phoneAnimator;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOpen = !isOpen;
            phoneAnimator.SetBool("IsOpen", isOpen);
        }
    }
}