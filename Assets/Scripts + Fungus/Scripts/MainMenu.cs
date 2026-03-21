using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Nombre de la escena del juego
    public string nombreEscenaJuego = "SampleScene";

    public static fadeInOut Instance;

    // Botón Jugar
    public void Jugar()
    {
        FadeAndLoad(nombreEscenaJuego);
    }

    // Botón Salir
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        fadeSalir();
    }

    public void Creditos()
    {
        FadeAndLoad("Creditos");
    }

    public void volverMenu()
    {
        FadeAndLoad("MainMenu");
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeAndLoadRoutine(sceneName));
    }

    private IEnumerator FadeAndLoadRoutine(string sceneName)
    {
        yield return StartCoroutine(fadeInOut.Instance.FadeOut());

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator fadeSalir()
    {
        yield return StartCoroutine(fadeInOut.Instance.FadeIn());
        Application.Quit();
}
}