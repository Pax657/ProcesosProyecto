using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration = 1f;

    public void FadeIn()
    {
        StartCoroutine(Fade(1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float target)
    {
        float start = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, target, time / duration);
            yield return null;
        }

        canvasGroup.alpha = target;
    }
}