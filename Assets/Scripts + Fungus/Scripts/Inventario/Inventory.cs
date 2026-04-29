using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus; // <- Importar Fungus

public class Inventory : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public List<InventorySlot> slots;

    public List<item> items = new List<item>();
    public item selectedItem;
    public int maxItems = 6;

    private bool isOpen = false;

    [Header("Animación")]
    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;
    public float speed = 10f;

    private Vector2 targetPosition;

    [Header("Cursor")]
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    [Header("Combinaciones")]
    public List<Combinacion> combinaciones;

    //[Header("Interacción")]
    private string interactableTag = "Interactuable";

    [Header("Fungus")]
    public Flowchart flowchart; // <- Referencia al Flowchart
    public string blockName = "InteractBlock"; // <- Nombre del bloque a ejecutar
    public string defaultBlock = "InteractBlock";

    public static Inventory instance;

    public void LoadFromData(InventoryData data)
    {
        maxItems = data.maxItems;
        items[0] = data.item0;
        items[1] = data.item1;
        items[2] = data.item2;
        items[3] = data.item3;
        items[4] = data.item4;
        items[5] = data.item5;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        inventoryPanel.anchoredPosition = hiddenPosition;
        targetPosition = hiddenPosition;
        UpdateUI();

        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        // Abrir/cerrar inventario
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            targetPosition = isOpen ? visiblePosition : hiddenPosition;
        }

        // Animación suave
        inventoryPanel.anchoredPosition = Vector2.Lerp(
            inventoryPanel.anchoredPosition,
            targetPosition,
            Time.deltaTime * speed
        );

        // CLICK EN EL MUNDO
        if (Input.GetMouseButtonDown(0))
        {
            CheckInteraction();
        }
    }

    public bool AddItem(item item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        items.Add(item);
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }
    }

    public bool HasItem(int index)
    {
        return index < items.Count;
    }

    public void SelectItem(int index)
    {
        selectedItem = items[index];
        Debug.Log("Seleccionado: " + selectedItem.itemName);

        if (selectedItem.cursor != null)
            CursorManager.instance.SetCursor(selectedItem.cursor, hotspot);
        else
            CursorManager.instance.SetCursor(defaultCursor, hotspot);
    }

    void DeselectItem()
    {
        selectedItem = null;
        CursorManager.instance.SetCursor(defaultCursor, hotspot);
    }

    void CheckInteraction()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero
        );

        Debug.Log("Raycast hit: " + (hit.collider != null ? hit.collider.name : "Nada"));
        if (hit.collider != null && hit.collider.CompareTag(interactableTag))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                bool usedItem = false;

                if (selectedItem != null)
                {
                    foreach (var interaction in interactable.itemInteractions)
                    {
                        if (interaction.itemName == selectedItem.itemName)
                        {
                            interactable.flowchart.ExecuteBlock(interaction.blockName);
                            usedItem = true;
                            break;
                        }
                    }
                }

                if (!usedItem && !string.IsNullOrEmpty(interactable.defaultBlock))
                {
                    interactable.flowchart.ExecuteBlock(interactable.defaultBlock);
                }
            }
        }
        else
        {
            DeselectItem();
        }
    }

    public void TryCombine(item draggedItem, item targetItem)
    {
        foreach (var combo in combinaciones)
        {
            bool match =
                (combo.itemA == draggedItem && combo.itemB == targetItem) ||
                (combo.itemA == targetItem && combo.itemB == draggedItem);

            if (match)
            {
                Debug.Log("Combinación correcta");

                //Ejecutar bloque de éxito
                if (flowchart != null && !string.IsNullOrEmpty(combo.successBlock))
                {
                    flowchart.ExecuteBlock(combo.successBlock);
                }

                //Eliminar items si toca
                if (combo.destroyA)
                    items.Remove(combo.itemA);

                if (combo.destroyB)
                    items.Remove(combo.itemB);

                //Añadir resultado
                if (combo.result != null)
                    AddItem(combo.result);

                UpdateUI();
                DeselectItem();
                return;
            }
        }

        //Combinacion incorrecta
        Debug.Log("Combinación incorrecta");

        if (flowchart != null && !string.IsNullOrEmpty(defaultBlock))
        {
            flowchart.ExecuteBlock(defaultBlock);
        }
    }
}