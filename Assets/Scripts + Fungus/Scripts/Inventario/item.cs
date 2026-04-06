using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class item : ScriptableObject
{
    public string itemName;
    public Sprite icon;          // Para UI (inventario)
    public Texture2D cursor;     // Para cursor
}
