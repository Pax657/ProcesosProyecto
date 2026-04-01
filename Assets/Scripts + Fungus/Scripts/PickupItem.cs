using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Sprite itemSprite; // el icono que aparecerá en el inventario
    private Inventory inventory;

    void Start()
    {
        // Busca el inventario en la escena
        inventory = FindObjectOfType<Inventory>();
    }

    void OnMouseDown()
    {
        // Cuando haces click sobre el objeto
        if (inventory != null)
        {
            bool added = inventory.AddItem(itemSprite);

            if (added)
            {
                // Si se añadió correctamente, eliminamos el objeto
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No se pudo recoger el objeto");
            }
        }
    }
}
