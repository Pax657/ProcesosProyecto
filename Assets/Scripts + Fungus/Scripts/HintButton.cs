using UnityEngine;

public class HintButton : MonoBehaviour
{
    public int coste;
    public GameObject pistaVisual; // texto o UI que muestra la pista

    public void ComprarPista()
    {
        if (PointsManager.Instance.SpendPoints(coste))
        {
            pistaVisual.SetActive(true);
        }
        else
        {
            Debug.Log("No tienes suficientes puntos");
        }
    }
}