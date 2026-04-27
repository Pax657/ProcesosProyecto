using UnityEngine;

public class InventoryData
{
    public int maxItems;
    public item item0;
    public item item1;
    public item item2;
    public item item3;
    public item item4;
    public item item5;

    public InventoryData(Inventory inventory)
    {
        maxItems = inventory.maxItems;
        item0 = inventory.items.Count > 0 ? inventory.items[0] : null;
        item1 = inventory.items.Count > 1 ? inventory.items[1] : null;
        item2 = inventory.items.Count > 2 ? inventory.items[2] : null;
        item3 = inventory.items.Count > 3 ? inventory.items[3] : null;
        item4 = inventory.items.Count > 4 ? inventory.items[4] : null;
        item5 = inventory.items.Count > 5 ? inventory.items[5] : null;
    }

}
