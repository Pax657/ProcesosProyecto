using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public RectTransform inventoryPanel; // cambiado
    public List<Image> slots;

    private List<Sprite> items = new List<Sprite>();
    private int maxItems = 6;

    private bool isOpen = false;

    [Header("Animación")]
    public Vector2 hiddenPosition;   // fuera (abajo)
    public Vector2 visiblePosition;  // dentro (visible)
    public float speed = 10f;

    private Vector2 targetPosition;

    void Start()
    {
        inventoryPanel.anchoredPosition = hiddenPosition;
        targetPosition = hiddenPosition;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;

            if (isOpen)
                targetPosition = visiblePosition;
            else
                targetPosition = hiddenPosition;
        }

        // Animación suave
        inventoryPanel.anchoredPosition = Vector2.Lerp(
            inventoryPanel.anchoredPosition,
            targetPosition,
            Time.deltaTime * speed
        );
    }

    public bool AddItem(Sprite itemSprite)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventario lleno");
            return false;
        }

        items.Add(itemSprite);
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].sprite = items[i];
                slots[i].enabled = true;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false;
            }
        }
    }
}