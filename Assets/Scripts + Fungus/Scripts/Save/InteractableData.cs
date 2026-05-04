using UnityEngine;
using System.Collections.Generic;
[System.Serializable]

public class InteractableStateData
{
    public string objectName;  // Para identificar el objeto
    public bool usado;

    public InteractableStateData(InteractablePoints interactable)
    {
        objectName = interactable.gameObject.name;
        usado = interactable.usado;
    }
}

[System.Serializable]
public class InteractableData
{
    public List<InteractableStateData> interactables = new List<InteractableStateData>();

    public InteractableData(InteractablePoints[] interactableArray)
    {
        foreach (InteractablePoints interactable in interactableArray)
        {
            interactables.Add(new InteractableStateData(interactable));
        }
    }
}