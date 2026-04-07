using UnityEngine;

public class Inspiracion : MonoBehaviour
{
    
    public RectTransform InspiracionPanel;
    private bool isActive;
    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;
    public float speed = 10f;
    private Vector2 targetPosition;
    public static Inspiracion instance;

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
        InspiracionPanel.anchoredPosition = hiddenPosition;
        targetPosition = hiddenPosition;
    }

    void Update()
    {
        // Abrir/cerrar inventario
        if (Input.GetKeyDown(KeyCode.R))
        {
            isActive = !isActive;
            targetPosition = isActive ? visiblePosition : hiddenPosition;
        }

        // Animación suave
        InspiracionPanel.anchoredPosition = Vector2.Lerp(
            InspiracionPanel.anchoredPosition,
            targetPosition,
            Time.deltaTime * speed
        );

        
    }
}
