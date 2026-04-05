using Fungus;
using UnityEngine;

public class UseItemOnObject : Clickable2D
{
    public Inventory inventory;
    public item requiredItem;

    public Flowchart flowchart;
    public string blockName;

    protected override void DoPointerClick()
    {
        base.DoPointerClick();

        if (inventory.selectedItem == null)
        {
            Debug.Log("No hay item seleccionado");
            return;
        }

        if (inventory.selectedItem == requiredItem)
        {
            Debug.Log("Item correcto!");

            flowchart.ExecuteBlock(blockName);

            inventory.selectedItem = null; // limpiar selección
        }
        else
        {
            Debug.Log("Item incorrecto");
        }
    }
}
