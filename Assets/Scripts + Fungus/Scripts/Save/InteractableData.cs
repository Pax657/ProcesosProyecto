using UnityEngine;

public class InteractableData
{
    public bool isUsed;

    public InteractableData(InteractablePoints interactablePoints)
    {
        isUsed = interactablePoints.usado;
    }
    
}
