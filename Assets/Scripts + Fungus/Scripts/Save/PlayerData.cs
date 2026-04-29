using UnityEngine;
[System.Serializable]

public class PlayerData
{
    public float currentX;
    public float currentY;

    public PlayerData(Movimiento movimiento)
    {
        currentX = movimiento.currentX;
        currentY = movimiento.currentY;
    }
}
