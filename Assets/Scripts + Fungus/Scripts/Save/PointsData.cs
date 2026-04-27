using UnityEngine;

public class PointsData
{
    public int puntos;

    public PointsData(PointsManager pointsManager)
    {
        puntos = pointsManager.puntos;
    }
}
