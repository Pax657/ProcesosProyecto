using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Vector2 seguir;
   

    public float speed;

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
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        seguir = transform.localPosition;
    }
}
