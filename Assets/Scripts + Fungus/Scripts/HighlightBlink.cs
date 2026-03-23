using UnityEngine;

public class HighlightBlink : MonoBehaviour
{
    private SpriteRenderer sr;

    public Color normalColor = Color.white;      // Color normal del sprite
    public Color highlightColor = Color.yellow;  // Color cuando parpadea
    public float blinkSpeed = 3f;                // Velocidad del parpadeo

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Calcula un valor entre 0 y 1 para interpolar entre los colores
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            sr.color = Color.Lerp(normalColor, highlightColor, t);
        }
        else
        {
            // Vuelve al color normal cuando no se presiona Ctrl
            sr.color = normalColor;
        }
    }
}