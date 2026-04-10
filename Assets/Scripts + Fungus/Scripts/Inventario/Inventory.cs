using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus; // <- Importar Fungus

public class Inventory : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public List<Image> slots;

    private List<item> items = new List<item>();
    public item selectedItem;
    private int maxItems = 6;

    private bool isOpen = false;

    [Header("Animación")]
    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;
    public float speed = 10f;

    private Vector2 targetPosition;

    [Header("Cursor")]
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    [Header("Interacción")]
    public string interactableTag = "Interactuable";

    [Header("Fungus")]
    public Flowchart flowchart; // <- Referencia al Flowchart
    public string blockName = "InteractBlock"; // <- Nombre del bloque a ejecutar
    public string defaultBlock = "InteractBlock";

    public static Inventory instance;

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
                slots[i].sprite = items[i].icon;
                slots[i].enabled = true;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false;
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
}