using UnityEngine;

public class PuzzlesData
{
    public bool usado;

    public PuzzlesData(Interactable interactable)
    {
        usado = interactable.usado;
    }
}
