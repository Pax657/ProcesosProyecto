using UnityEngine;

public class PuzzlesData
{
    public bool usado;

    public PuzzlesData(Interactable interactable)
    {
        usado = interactable.usado;
    }

    public void LoadFromData(PuzzlesData data)
    {
        this.usado = data.usado;
    }
}
