using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Asigna aquí tu panel desde el inspector
    private bool isPaused = false;
    public Movimiento jugador;

    private Animator animator;

    void Start()
    {
        pausePanel.SetActive(false); // Asegura que empieza oculto
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            animator = GetComponent<Animator>(); // Asegúrate de que el componente Animator esté asignado
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        jugador.seguir = jugador.transform.localPosition; // Detiene el movimiento del jugador al pausar
        animator.SetBool("IsPaused", isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa el juego
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el juego
        }


    }
}