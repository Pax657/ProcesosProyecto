using UnityEngine;
[System.Serializable]

public class InteractableData
{
    public bool usado;

    public InteractableData(InteractablePoints interactable)
    {
        usado = interactable.usado;
    }
}