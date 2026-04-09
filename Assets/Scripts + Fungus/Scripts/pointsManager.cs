using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    public int puntos = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(int cantidad)
    {
        puntos += cantidad;
        UIManager.Instance.UpdatePointsText(puntos);
    }

    public bool SpendPoints(int cantidad)
    {
        if (puntos >= cantidad)
        {
            puntos -= cantidad;
            UIManager.Instance.UpdatePointsText(puntos);
            return true;
        }
        return false;
    }
}