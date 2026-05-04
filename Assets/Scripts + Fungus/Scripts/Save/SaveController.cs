using UnityEngine;

public class SaveController : MonoBehaviour
{
    public PointsManager pointsManager;
    public Movimiento movimiento;
    public Inventory inventory;
    public Interactable interactable;
    public InteractablePoints interactablePoints;

    public void SaveGame()
    {
        SaveManager.SaveGlobal(
            pointsManager,
            movimiento,
            inventory,
            interactable,
            interactablePoints
        );

        Debug.Log("Guardado");
    }
}