using Fungus;
using UnityEngine;

public class PickupItem : Clickable2D
{
    [Header("Item")]
    public item item;

    [Header("Referencia Inventario")]
    public Inventory inventory;

    [Header("Comportamiento")]
    public bool destroyOnPickup = true;

    public bool itemNoTomado = true;


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

        if (item == null)
        {
            Debug.LogWarning("Sprite no asignado en " + gameObject.name);
            return;
        }

        if (itemNoTomado)
        {
            inventory.AddItem(item);

            Debug.Log("Item añadido al inventario");

            itemNoTomado = false;

            if (destroyOnPickup)
            {
                Destroy(gameObject);
            }

            else
            {
                Debug.Log("No se pudo añadir el item (inventario lleno)");
            }
        }

    }
}