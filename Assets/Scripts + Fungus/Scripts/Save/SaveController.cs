using UnityEngine;

public class SaveController : MonoBehaviour
{
    private PointsManager pointsManager;
    private Movimiento movimiento;
    private Inventory inventory;
    private Interactable interactable;
    private InteractablePoints interactablePoints;

    void Start()
    {
        pointsManager = FindObjectOfType<PointsManager>();
        movimiento = FindObjectOfType<Movimiento>();
        inventory = FindObjectOfType<Inventory>();
        interactable = FindObjectOfType<Interactable>();
        interactablePoints = FindObjectOfType<InteractablePoints>();
    }

    public void LoadGame()
    {
        SaveManager.LoadGlobal(
            pointsManager,
            movimiento,
            inventory,
            interactable,
            interactablePoints
        );

        Debug.Log("📂 Cargado en escena actual");
    }

    public void SaveGame()
    {
        SaveManager.SaveGlobal(
            pointsManager,
            movimiento,
            inventory,
            interactable,
            interactablePoints
        );

        Debug.Log("💾 Guardado");
    }
}