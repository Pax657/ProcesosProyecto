using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class arrastrarBotones : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string letter;

    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector2 originalPosition;
    private hueco currentSlot;
    private bool isDragging;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        // Liberar slot si estaba en uno
        if (currentSlot != null)
        {
            currentSlot.currentLetter = null;
            currentSlot = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        hueco closestSlot = FindClosestSlot();

        if (closestSlot != null && !closestSlot.IsOccupied())
        {
            SnapToSlot(closestSlot);
            puzzleManager.Instance.CheckPuzzle();
        }
        else
        {
            ReturnToStart();
        }
    }

    void SnapToSlot(hueco slot)
    {
        currentSlot = slot;
        slot.currentLetter = this;

        rectTransform.position = slot.transform.position;
    }

    public void ReturnToStart()
    {
        if (currentSlot != null)
        {
            currentSlot.currentLetter = null;
            currentSlot = null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }

    hueco FindClosestSlot()
    {
        hueco[] slots = FindObjectsOfType<hueco>();

        float minDist = 100f;
        hueco closest = null;

        foreach (var slot in slots)
        {
            float dist = Vector2.Distance(rectTransform.position, slot.transform.position);

            if (dist < 100f && dist < minDist)
            {
                minDist = dist;
                closest = slot;
            }
        }

        return closest;
    }

    public void ReturnToStartSmooth()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothReturn());
    }

    IEnumerator SmoothReturn()
    {
        if (currentSlot != null)
        {
            currentSlot.currentLetter = null;
            currentSlot = null;
        }

        Vector2 startPos = rectTransform.anchoredPosition;
        float time = 0f;
        float duration = 0.4f;

        while (time < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, originalPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}