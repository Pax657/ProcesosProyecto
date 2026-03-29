using Fungus;
using System.Collections;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [HideInInspector]
    public Vector2 seguir;
    public bool inDialogue;

    private SpriteRenderer spriteRenderer;

    [Space(10)]

    public float speed;

 

    [Space(20)]

    public Transform yCerca;
    public Transform yLejos;
    private Vector3 escalaInicial;

    [Space(20)]

    public float perspectiva;
    public float ratioPerspectiva;
    public float minPers, maxPers;

    [Space(20)]

    public Texture2D idleCursor;
    public Texture2D interactCursor;
    private Texture2D currentCursor;
    
    [Space(30)]

    public LayerMask floorLayer;
    public LayerMask interactableLayer;

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
            UpdateCursor();
        }
        cambioPerspectiva();
        ajusteLayers();
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        seguir = transform.localPosition;
    }
    */

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
        float t = Mathf.InverseLerp(yLejos.position.y, yCerca.position.y, transform.position.y);

        spriteRenderer.sortingOrder = Mathf.RoundToInt(Mathf.Lerp(-10, 100, t));
    }

    private void clickMoverse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            //Revisamos primero interactuables
            Collider2D hitInteractable = Physics2D.OverlapPoint(mousePos2D, interactableLayer);

            if (hitInteractable != null)
            {
                Clickable2D clickable = hitInteractable.GetComponent<Clickable2D>();

                if (clickable != null)
                {
                    StartCoroutine(MoveAndInteract(clickable));
                    return;
                }
            }

            //Tras esto, revisamos si es suelo
            RaycastHit2D hitFloor = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, floorLayer);

            if (hitFloor.collider != null)
            {
                seguir = hitFloor.collider.ClosestPoint(mousePos2D);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, seguir, speed * Time.deltaTime);
    }

    public void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        float distance = Vector2.Distance(transform.position, seguir);

        if (distance < 0.8f) //Evitamos vibrar si no podemos pasar
        {
            seguir = transform.position;
        }
    }

    private IEnumerator MoveAndInteract(Clickable2D target)
    {
        Vector2 targetPos = target.transform.position;

        // Move towards object (or later: interaction point)
        seguir = targetPos;

        while (Vector2.Distance(transform.position, targetPos) > target.activateDistance)
        {
            yield return null;
        }

        // Trigger Fungus click AFTER reaching distance
        target.SendMessage("DoPointerClick", SendMessageOptions.DontRequireReceiver);
    }

    private void SetCursor(Texture2D newCursor)
    {
        if (currentCursor == newCursor) return;

        Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = newCursor;
    }

    private void UpdateCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hitInteractable = Physics2D.OverlapPoint(mousePos, interactableLayer);

        if (hitInteractable != null)
        {
            SetCursor(interactCursor);
            return;
        }

        SetCursor(idleCursor);
    }

    public void exitDialogue()
    {
        inDialogue = false;
    }

    public void startDialogue()
    {
        inDialogue = true;
    }
}
