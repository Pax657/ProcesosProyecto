using UnityEngine;

public class hueco : MonoBehaviour
{
    public arrastrarBotones currentLetter;

    public bool IsOccupied()
    {
        return currentLetter != null;
    }
}
