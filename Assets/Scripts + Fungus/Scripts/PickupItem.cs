using UnityEngine;
using Fungus;

public class PickupItem : Clickable2D
{
    [Header("Item")]
    public Sprite itemSprite;

    [Header("Referencia Inventario")]
    public Inventory inventory;

    [Header("Comportamiento")]
    public bool destroyOnPickup = true;

    protected override void DoPointerClick()
    {
        base.DoPointerClick(); // mantiene compatibilidad con Fungus

        if (!clickEnabled)
            return;

        if (inventory == null)
        {
            Debug.LogWarning("Inventory no asignado en " + gameObject.name);
            return;
        }

        if (itemSprite == null)
        {
            Debug.LogWarning("Sprite no asignado en " + gameObject.name);
            return;
        }

        bool added = inventory.AddItem(itemSprite);

        if (added)
        {
            Debug.Log("Item añadido al inventario");

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("No se pudo añadir el item (inventario lleno)");
        }
    }
}