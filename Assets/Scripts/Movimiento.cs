using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Vector2 seguir;
   

    public float speed;

    public float perspectiva;
    public float ratioPerspectiva;
    public float minPers, maxPers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seguir = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            seguir = new Vector2(mousePosition.x, mousePosition.y);
        }

        transform.localPosition = Vector2.MoveTowards(transform.position, seguir, speed*Time.deltaTime);

        cambioPerspectiva();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        seguir = transform.localPosition;
    }

    private void cambioPerspectiva()
    {
        Vector3 escalaActual = this.transform.localScale;

        float nuevaPerspectiva = perspectiva * (ratioPerspectiva - transform.position.y);
        nuevaPerspectiva = Mathf.Clamp(nuevaPerspectiva, minPers, maxPers);

        escalaActual.x = nuevaPerspectiva;
        escalaActual.y = nuevaPerspectiva;

        transform.localScale = escalaActual;
    }
}
