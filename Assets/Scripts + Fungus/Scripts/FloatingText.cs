using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float speed = 1f;
    public float duration = 1f;

    public TextMeshProUGUI text; // asignado en inspector

    private float timer;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        timer += Time.deltaTime;

        Color c = text.color;
        c.a = Mathf.Lerp(1, 0, timer / duration);
        text.color = c;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}