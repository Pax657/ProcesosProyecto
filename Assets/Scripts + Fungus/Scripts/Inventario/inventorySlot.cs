using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour,
    IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int index;
    public Inventory inventory;
    public Image image;

    public item currentItem;

    private Transform originalParent;
    private Canvas canvas;
    private Vector3 originalPosition;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetItem(item newItem)
    {
        currentItem = newItem;

        if (newItem != null)
        {
            image.sprite = newItem.icon;
            image.enabled = true;
        }
        else
        {
            image.sprite = null;
            image.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.HasItem(index))
        {
            Debug.Log("CLICK EN SLOT " + index);
            inventory.SelectItem(index);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        transform.SetParent(canvas.transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        image.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = originalPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

        Debug.Log("OnDrop llamado en: " + gameObject.name);
        if (draggedSlot == null)
        {
            Debug.Log("❌ draggedSlot es NULL");
            return;
        }

        if (draggedSlot != null &&
            draggedSlot.currentItem != null &&
            currentItem != null)
        {
            inventory.TryCombine(draggedSlot.currentItem, currentItem);
        }
    }
}