using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public RectTransform pausePanel; // Mejor usar RectTransform para UI
    public Movimiento jugador;

    public bool isPaused = false;

    public Vector2 hiddenPosition = new Vector2(0, 800); // fuera de pantalla (arriba)
    public Vector2 visiblePosition = new Vector2(0, 0);  // posición visible
    public float speed = 10f;

    private Vector2 targetPosition;

    void Start()
    {
        pausePanel.anchoredPosition = hiddenPosition;
        targetPosition = hiddenPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Movimiento suave del panel
        pausePanel.anchoredPosition = Vector2.Lerp(
            pausePanel.anchoredPosition,
            targetPosition,
            Time.unscaledDeltaTime * speed
        );
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            targetPosition = visiblePosition; // baja
            Time.timeScale = 0f;

        }
        else
        {
            targetPosition = hiddenPosition; // sube
            Time.timeScale = 1f;

        }

    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
        Debug.Log("CLICK EN OPCIONES");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; //Asegurarse de que el tiempo se reanude al volver al menú principal
        Debug.Log("CLICK EN MENU");
    }

    public void Resumir()
    {
        Debug.Log("CLICK EN RESUMIR");
        isPaused = false;
        targetPosition = hiddenPosition;
        Time.timeScale = 1f;
        jugador.seguir = jugador.transform.localPosition;
    }
}