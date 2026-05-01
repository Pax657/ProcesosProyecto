using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class puzzleManagerArmario : MonoBehaviour
{
    public static puzzleManagerArmario Instance;

    public hueco[] slots;
    public string correctWord = "ROSA";

    public bool active = false;

    public Canvas canvas;
    public GameObject armario;

    public Movimiento jugador;

    public List<RewardEntry> rewards = new List<RewardEntry>();

    public Inventory inventory;
    private bool rewardGiven = false;

    public Flowchart flowchart;
    public string nombreBloque;

    private cambiarModo cambiar;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cambiar = GetComponent<cambiarModo>();
        active = false;
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(active) jugador.enabled = false;
         else jugador.enabled = true;
    }

    public void ActivarPuzle()
    {
        active = true;
        cambiar.activarNormal();
        canvas.gameObject.SetActive(true);
    }

    public void DesactivarPuzle()
    {
        active = false;
        cambiar.puzzleTerminar();
        canvas.gameObject.SetActive(false);
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

            if (!rewardGiven)
            {
                foreach (var reward in rewards)
                {
                    for (int i = 0; i < reward.amount; i++)
                    {
                        inventory.AddItem(reward.item);
                    }
                }

                rewardGiven = true;
            }


            DesactivarPuzle();
            gameObject.SetActive(false);
            
            flowchart.ExecuteBlock(nombreBloque);
            
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