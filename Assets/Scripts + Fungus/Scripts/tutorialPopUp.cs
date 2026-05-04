using System.Collections;
using UnityEngine;
using TMPro;

public class tutorialPopUp : MonoBehaviour
{
    public RectTransform panel;
    public TMP_Text texto;

    public float speed = 500f;

    private Vector2 hiddenPos;
    private Vector2 visiblePos;

    private bool isShowing;

    void Awake()
    {
        visiblePos = new Vector2(-257, 127);
        hiddenPos = new Vector2(-525, 127);

        panel.anchoredPosition = hiddenPos;
    }

    public void MostrarPopup(string mensaje)
    {
        texto.text = mensaje;
        StopAllCoroutines();
        StartCoroutine(MoverPopup(visiblePos));
        isShowing = true;
    }

    public void OcultarPopup()
    {
        StopAllCoroutines();
        StartCoroutine(MoverPopup(hiddenPos));
        Debug.Log("click en ocultar");
        isShowing = false;
    }

    IEnumerator MoverPopup(Vector2 target)
    {
        while (Vector2.Distance(panel.anchoredPosition, target) > 0.1f)
        {
            panel.anchoredPosition = Vector2.MoveTowards(
                panel.anchoredPosition,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        panel.anchoredPosition = target;
    }
}
