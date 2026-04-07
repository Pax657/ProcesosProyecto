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
}