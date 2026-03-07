using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Nombre de la escena del juego
    public string nombreEscenaJuego = "SampleScene";

    // Botón Jugar
    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    // Botón Salir
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void volverMenu()
    {
        Debug.Log("Volviendo al menu...");
        SceneManager.LoadScene("MainMenu");
    }
}