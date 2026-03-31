using UnityEngine;
using System.Collections;

public class TitleCard : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float duration;
    public float time;

    public void Start()
    {
        //canvasGroup.alpha = 0.0f;
    }
    public void PlayTitle()
    {
        StartCoroutine(PlayTitle(duration, time));
    }

    public IEnumerator PlayTitle(float fadeDuration, float waitTime)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(waitTime);

        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
