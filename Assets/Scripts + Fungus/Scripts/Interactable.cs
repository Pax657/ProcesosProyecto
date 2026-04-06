using UnityEngine;
using Fungus;

public class Interactable : MonoBehaviour
{
    [Header("Fungus")]
    public Flowchart flowchart;         // Flowchart asociado a este objeto
    public string defaultBlock;         // Bloque cuando no hay item
    public string itemBlock;            // Bloque cuando hay un item seleccionado
}