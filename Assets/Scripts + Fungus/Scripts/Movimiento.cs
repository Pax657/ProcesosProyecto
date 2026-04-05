using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movimiento : MonoBehaviour
{
    [HideInInspector]
    public Vector2 seguir;
    public bool inDialogue;
    public bool puedeMover;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Space(10)]

    public float speed;

    [Space(15)]

    [Header("Escala")]
    public Transform yCerca;
    public Transform yLejos;
    private Vector3 escalaInicial;

    [Space(15)]

    [Header("Perspectiva")]
    public float perspectiva;
    public float ratioPerspectiva;
    public float minPers, maxPers;

    [Space(15)]

    [Header("Cursores")]
    public Texture2D idleCursor;
    public Texture2D interactCursor;
    private Texture2D currentCursor;

    [Space(20)]

    [Header("Layers")]
    public LayerMask floorLayer;
    public LayerMask interactableLayer;
    public LayerMask obstacleLayer;

    private PauseMenu pauseMenu;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("caminando", false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seguir = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();

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

        comprobarPausa();
        cambioPerspectiva();
        ajusteLayers();


        if (inDialogue) animator.SetBool("caminando", false);
    }

    public void cambioPerspectiva()
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
        // Detectar click izquierdo
        if (Input.GetMouseButtonDown(0))
        {

            /* if (EventSystem.current.IsPointerOverGameObject())
                 return;
            */
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // Primero revisamos interactuables
            Collider2D hitInteractable = Physics2D.OverlapPoint(mousePos2D, interactableLayer);
            if (hitInteractable != null)
            {
                Clickable2D clickable = hitInteractable.GetComponent<Clickable2D>();
                if (clickable != null)
                {
                    if (puedeMover)
                    {
                        StartCoroutine(MoveAndInteract(clickable));
                        return;
                    }
                }
            }

            // Revisamos si es suelo
            RaycastHit2D hitFloor = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, floorLayer);
            if (hitFloor.collider != null)
            {
                // Actualizamos el objetivo al click
                seguir = hitFloor.collider.ClosestPoint(mousePos2D);
            }
        }

        Vector2 currentPos = transform.position;
        Vector2 direction = seguir - currentPos;
        Vector2 rawDir = seguir - currentPos;

        // Evitamos el offset inicial que generaba movimiento “fantasma”
        float distance = direction.magnitude;
        if (distance > 0.01f)
        {
            // Aplicamos el offset solo si ya hay un objetivo distinto de la posición actual
            float offsetY = spriteRenderer.bounds.extents.y - 0.2f;
            direction.y += offsetY;
        }

        // 🔹 DISTANCIA REAL (con offset aplicado)
        float realDistance = direction.magnitude;

        // Flip del sprite según dirección
        if (Mathf.Abs(direction.x) > 0.01f)
            spriteRenderer.flipX = direction.x > 0;

        // Movimiento
        if (puedeMover)
        {
            if (realDistance > 0.01f)
            {
                RaycastHit2D hit = Physics2D.Raycast(currentPos, direction.normalized, realDistance, obstacleLayer);
                Vector2 moveDelta;

                if (hit.collider == null)
                {
                    moveDelta = direction.normalized * speed * Time.deltaTime;
                }
                else
                {
                    // Deslizamiento en caso de obstáculo
                    float slideFactor = 0.5f;
                    Vector2 tangent = Vector2.Perpendicular(hit.normal);
                    Vector2 slideDir = Vector2.Dot(direction, tangent) * tangent.normalized * slideFactor;
                    moveDelta = (direction.normalized + slideDir).normalized * speed * Time.deltaTime;
                }

                transform.position = currentPos + moveDelta;
            }

            if (realDistance > 0.1f)
            {
                animator.SetBool("caminando", true);
                //Debug.Log("Caminando hacia: " + seguir);
            }
            else
            {
                animator.SetBool("caminando", false);
                //Debug.Log("Detenido en: " + seguir);
            }
        }
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
        //Revisa elementos de la UI primero
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponentInParent<Clickable2D>() != null)
            {
                SetCursor(interactCursor);
                return;
            }
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hitInteractable = Physics2D.OverlapPoint(mousePos, interactableLayer);

        if (hitInteractable != null)
        {
            SetCursor(interactCursor);
            return;
        }

        SetCursor(idleCursor);
    }

    private void comprobarPausa()
    {
        if (pauseMenu.isPaused) puedeMover = false;
        if (!pauseMenu.isPaused && !inDialogue) puedeMover = true;
    }
    public void exitDialogue()
    {
        inDialogue = false;
        puedeMover = true;
    }

    public void startDialogue()
    {
        inDialogue = true;
        puedeMover = false;
    }
}
