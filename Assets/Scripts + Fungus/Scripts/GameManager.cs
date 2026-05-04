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
            var loader = FindObjectOfType<LoadGameApplier>();
            if (loader != null)
                loader.ApplySave();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}