using Fungus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IDropHandler
{
    public Flowchart flowchart;
    public string defaultBlock;

    public List<ItemInteraction> itemInteractions;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop sobre interactuable");

        InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (draggedSlot == null || draggedSlot.currentItem == null)
            return;

        item draggedItem = draggedSlot.currentItem;

        bool usedItem = false;

        foreach (var interaction in itemInteractions)
        {
            if (interaction.itemName == draggedItem.itemName)
            {
                flowchart.ExecuteBlock(interaction.blockName);
                usedItem = true;
                break;
            }
        }

        if (!usedItem && !string.IsNullOrEmpty(defaultBlock))
        {
            flowchart.ExecuteBlock(defaultBlock);
        }
    }
}