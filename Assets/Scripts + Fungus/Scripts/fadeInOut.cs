using UnityEngine;
using System.Collections;

public class fadeInOut : MonoBehaviour
{
    public static fadeInOut Instance;

    public CanvasGroup canvasGroup;
    public float fadeInSpeed = 0.5f;
    public float fadeOutSpeed = 2f;

    private void Start()
    {
        StartCoroutine(fadeInOut.Instance.FadeIn());
    }


    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeIn()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeInSpeed;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeOutSpeed;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}