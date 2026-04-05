using UnityEngine;
using System.Collections;

public class fadeInOut : MonoBehaviour
{
    public static fadeInOut Instance;

    public CanvasGroup canvasGroup;
    public float fadeInSpeed;
    public float fadeOutSpeed;

    private void Start()
    {
        StartCoroutine(fadeInOut.Instance.FadeIn(fadeInSpeed));
    }


    private void Awake()
    {
        Instance = this;
        StartCoroutine(fadeInOut.Instance.FadeIn(fadeInSpeed));
    }

    public IEnumerator FadeIn(float fadeIn)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeIn;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    public IEnumerator FadeOut(float fadeOut)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeOut;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}