using Fungus;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Fungus")]
    public Flowchart flowchart;         // Flowchart asociado a este objeto
    public string defaultBlock;         // Bloque cuando no hay item

    [Header("Interacciones por item")]
    public List<ItemInteraction> itemInteractions;

    public int puntos = 10;
    private bool usado = false;

    public void Interactuar()
    {
        if (!usado)
        {
            PointManager.Instance.AddPoints(puntos);
            usado = true;
        }
    }
}