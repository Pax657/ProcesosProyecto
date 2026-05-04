using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public PointsManager pointsManager;
    public Movimiento movimiento;
    public Inventory inventory;
    public Interactable interactables;
    public InteractablePoints interactablePoints;

    private void Awake()
    {

        ApplySave();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 💾 GUARDAR
    public void SaveGame()
    {
        SaveManager.SaveGlobal(
            pointsManager,
            movimiento,
            inventory,
            interactables,
            interactablePoints
        );

        Debug.Log("Juego guardado");
    }

    void ApplySave()
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