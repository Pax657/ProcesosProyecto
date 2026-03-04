using UnityEngine;
using Fungus;

[RequireComponent(typeof(Character))]
public class characterAnchor : MonoBehaviour
{
    public Transform dialogueAnchor;

    public Transform GetAnchor()
    {
        return dialogueAnchor;
    }
}
