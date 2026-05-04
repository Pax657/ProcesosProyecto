using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public PointsManager pointsManager;
    public Movimiento movimiento;
    public Inventory inventory;
    public Interactable interactable;
    public InteractablePoints interactablePoints;

    private void Awake()
    {
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
            interactable,
            interactablePoints
        );

        Debug.Log("💾 Juego guardado");
    }

    // 📂 CARGAR (desde gameplay)
    //public void LoadGame()
    //{
    //    SaveManager.LoadGlobal(
    //        pointsManager,
    //        movimiento,
    //        inventory,
    //        interactable,
    //        interactablePoints
    //    );
    //
    //    Debug.Log("📂 Juego cargado");
    //}
}