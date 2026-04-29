using Fungus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IDropHandler
{
    public Flowchart flowchart;
    public string defaultBlock;

    public List<ItemInteraction> itemInteractions;

    public bool usado;

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
            if (usado) return;
            if (interaction.itemName == draggedItem.itemName)
            {
                flowchart.ExecuteBlock(interaction.blockName);
                usedItem = true;
                break;
                usado = true;
            }
        }

        if (!usedItem && !string.IsNullOrEmpty(defaultBlock))
        {
            flowchart.ExecuteBlock(defaultBlock);
        }
    }

    public void LoadFromData(PuzzlesData data)
    {
        usado = data.usado;
    }
}