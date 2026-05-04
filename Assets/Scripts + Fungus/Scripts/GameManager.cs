using UnityEngine;

public class GameManager : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
=======
    public static GameManager Instance;

    [Header("References")]
    public PointsManager pointsManager;
    public Movimiento movimiento;
    public Inventory inventory;
    Interactable[] interactables;
    InteractablePoints[] interactablePoints;

    private void Awake()
>>>>>>> Stashed changes
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        SaveManager.SaveGlobal(
            pointsManager,
            movimiento,
            inventory,
            interactable,
            interactablePoints
        );

        Debug.Log("Juego guardado");
>>>>>>> Stashed changes
    }
}
