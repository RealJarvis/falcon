using System.Collections;
using UnityEngine;

public class MenuPanelsController : MonoBehaviour
{
    [SerializeField] private RectTransform mainPanel;
    [SerializeField] private RectTransform prehistoryPanel;
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float panelOffset = 1920f;

    private bool isAnimating = false;

    private void Start()
    {
        mainPanel.anchoredPosition = Vector2.zero;
        prehistoryPanel.anchoredPosition = new Vector2(-panelOffset, 0f);
        controlsPanel.anchoredPosition = new Vector2(panelOffset, 0f);
    }

    public void OpenPrehistory()
    {
        if (isAnimating) return;
        StartCoroutine(SlidePanels(mainPanel, prehistoryPanel, Vector2.zero, new Vector2(-panelOffset, 0f), new Vector2(-panelOffset, 0f), Vector2.zero));
    }

    public void OpenControls()
    {
        if (isAnimating) return;
        StartCoroutine(SlidePanels(mainPanel, controlsPanel, Vector2.zero, new Vector2(panelOffset, 0f), new Vector2(panelOffset, 0f), Vector2.zero));
    }

    public void BackFromPrehistory()
    {
        if (isAnimating) return;
        StartCoroutine(SlidePanels(prehistoryPanel, mainPanel, Vector2.zero, new Vector2(panelOffset, 0f), new Vector2(-panelOffset, 0f), Vector2.zero));
    }

    public void BackFromControls()
    {
        if (isAnimating) return;
        StartCoroutine(SlidePanels(controlsPanel, mainPanel, Vector2.zero, new Vector2(-panelOffset, 0f), new Vector2(panelOffset, 0f), Vector2.zero));
    }

    private IEnumerator SlidePanels(
        RectTransform outgoingPanel,
        RectTransform incomingPanel,
        Vector2 outgoingStart,
        Vector2 outgoingEnd,
        Vector2 incomingStart,
        Vector2 incomingEnd)
    {
        isAnimating = true;

        outgoingPanel.anchoredPosition = outgoingStart;
        incomingPanel.anchoredPosition = incomingStart;

        float timer = 0f;

        while (timer < slideDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / slideDuration);

            outgoingPanel.anchoredPosition = Vector2.Lerp(outgoingStart, outgoingEnd, t);
            incomingPanel.anchoredPosition = Vector2.Lerp(incomingStart, incomingEnd, t);

            yield return null;
        }

        outgoingPanel.anchoredPosition = outgoingEnd;
        incomingPanel.anchoredPosition = incomingEnd;

        isAnimating = false;
    }
}