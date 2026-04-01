using System.Collections;
using UnityEngine;

public class puzzleManager : MonoBehaviour
{
    public static puzzleManager Instance;

    public hueco[] slots;
    public string correctWord = "ROSA";

    public bool active = false;
    private bool completado = false;

    public Canvas canvas;
    public GameObject armario;

    public Movimiento jugador;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        active = false;
        canvas.gameObject.SetActive(false);
    }

    public void ActivarPuzle()
    {
        active = true;
        armario.SetActive(false);
        canvas.gameObject.SetActive(true);
        jugador.inDialogue = true;
    }

    public void DesactivarPuzle()
    {
        active = false;
        armario.SetActive(true);
        canvas.gameObject.SetActive(false);
        jugador.inDialogue = false;
    }

    public void CheckPuzzle()
    {
        //comprobar si todos los slots están llenos
        foreach (var slot in slots)
        {
            if (!slot.IsOccupied())
                return;
        }

        //construir palabra
        string formedWord = "";

        foreach (var slot in slots)
        {
            formedWord += slot.currentLetter.letter;
        }

        Debug.Log("Palabra: " + formedWord);

        if (formedWord == correctWord)
        {
            Debug.Log("Correcto");
            completado = true;
            DesactivarPuzle();
        }
        else
        {
            Debug.Log("Incorrecto");
            ResetAll();
        }
    }

    void ResetAll()
    {
        StartCoroutine(ResetAnimation());
    }

    IEnumerator ResetAnimation()
    {
        //Vibrar huecos
        foreach (var slot in slots)
        {
            StartCoroutine(Shake(slot.transform));
        }

        //Vibrar letras
        arrastrarBotones[] letters = FindObjectsOfType<arrastrarBotones>();

        foreach (var letter in letters)
        {
            StartCoroutine(Shake(letter.transform));
        }

        yield return new WaitForSeconds(0.4f);

        //Mover letras a huecos
        foreach (var letter in letters)
        {
            letter.ReturnToStartSmooth();
        }
    }

    IEnumerator Shake(Transform target)
    {
        Vector3 originalPos = target.localPosition;

        float duration = 0.4f;
        float strength = 10f;

        float time = 0;

        while (time < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * strength;
            target.localPosition = originalPos + new Vector3(offsetX, 0, 0);

            time += Time.deltaTime;
            yield return null;
        }

        target.localPosition = originalPos;
    }
}