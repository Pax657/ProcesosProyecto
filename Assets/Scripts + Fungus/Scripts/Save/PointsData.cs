using UnityEngine;
[System.Serializable]

public class PointsData
{
    public int puntos;

    public PointsData(PointsManager pointsManager)
    {
        puntos = pointsManager.puntos;
    }

    
}
