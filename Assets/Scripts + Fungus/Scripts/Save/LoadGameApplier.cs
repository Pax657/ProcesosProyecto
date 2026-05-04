using UnityEngine;

public class LoadGameApplier : MonoBehaviour
{
    void Start()
    {
        ApplySave();
    }

    public void ApplySave()
    {
        var player = FindObjectOfType<Movimiento>();
        var points = FindObjectOfType<PointsManager>();
        var inventory = FindObjectOfType<Inventory>();
        var interactable = FindObjectOfType<Interactable>();
        var interactablePoints = FindObjectOfType<InteractablePoints>();

        if (SaveDataCache.playerData != null)
            player.LoadFromData(SaveDataCache.playerData);

        if (SaveDataCache.pointsData != null)
            points.LoadFromData(SaveDataCache.pointsData);

        if (SaveDataCache.inventoryData != null)
            inventory.LoadFromData(SaveDataCache.inventoryData);

        if (SaveDataCache.puzzlesData != null)
            interactable.LoadFromData(SaveDataCache.puzzlesData);

        if (SaveDataCache.interactableData != null)
            interactablePoints.LoadFromData(SaveDataCache.interactableData);
    }
}