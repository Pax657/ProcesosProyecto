using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saltoEscena : MonoBehaviour
{
    // Nombre de la escena del juego
    public string nombreEscena = "SampleScene";

    public static fadeInOut Instance;

    public void cambioEscena()
    {
        FadeAndLoad(nombreEscena);
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
}
