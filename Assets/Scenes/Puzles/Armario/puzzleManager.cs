using System.Collections;
using UnityEngine;

public class puzzleManager : MonoBehaviour
{
    public static puzzleManager Instance;

    public hueco[] slots;
    public string correctWord = "ROSA";

    void Awake()
    {
        Instance = this;
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
        // 🔴 1. Vibrar slots
        foreach (var slot in slots)
        {
            StartCoroutine(Shake(slot.transform));
        }

        // 🔴 2. Vibrar letras
        arrastrarBotones[] letters = FindObjectsOfType<arrastrarBotones>();

        foreach (var letter in letters)
        {
            StartCoroutine(Shake(letter.transform));
        }

        // ⏳ Esperar a que termine el shake
        yield return new WaitForSeconds(0.4f);

        // 🔻 3. Liberar slots + volver letras suavemente
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