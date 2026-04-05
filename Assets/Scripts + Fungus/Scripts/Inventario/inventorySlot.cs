using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public Inventory inventory;
    public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.HasItem(index))
        {
            Debug.Log("CLICK EN SLOT " + index);
            inventory.SelectItem(index);
        }
    }
}
