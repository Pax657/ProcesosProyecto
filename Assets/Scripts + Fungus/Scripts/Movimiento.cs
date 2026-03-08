using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [HideInInspector]
    public Vector2 seguir;
    public bool inDialogue;

    private SpriteRenderer spriteRenderer;
   

    public float speed;

    private Vector3 escalaInicial;

    public Transform yCerca;
    public Transform yLejos;

    public float perspectiva;
    public float ratioPerspectiva;
    public float minPers, maxPers;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seguir = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        escalaInicial = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue)
        {
            clickMoverse();
        }
        cambioPerspectiva();
        ajusteLayers();
        Debug.Log(this.transform.localScale);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        seguir = transform.localPosition;
    }

    private void cambioPerspectiva()
    {
        float t = Mathf.InverseLerp(yLejos.position.y, yCerca.position.y, transform.position.y);

        float nuevaPerspectiva = Mathf.Lerp(minPers, maxPers, t);

        Vector3 nuevaEscala = escalaInicial;
        nuevaEscala.x = escalaInicial.x * nuevaPerspectiva;
        nuevaEscala.y = escalaInicial.y * nuevaPerspectiva;

        transform.localScale = nuevaEscala;
    }

    private void ajusteLayers()
    {
        spriteRenderer.sortingOrder = (int)transform.position.y * -10;
    }

    private void clickMoverse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            seguir = new Vector2(mousePosition.x, mousePosition.y);
        }

        transform.localPosition = Vector2.MoveTowards(transform.position, seguir, speed * Time.deltaTime);
    }

    public void exitDialogue()
    {
        inDialogue = false;
    }
}
