using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saltoEscena : MonoBehaviour
{
    //Nombre de la escena
    public string nombreEscena = "SampleScene";

    public void cambioEscena()
    {
        Debug.Log("Cambio escena a: " + nombreEscena);

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