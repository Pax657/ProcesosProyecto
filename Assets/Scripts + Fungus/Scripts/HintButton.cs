using UnityEngine;

public class HintButton : MonoBehaviour
{
    public int coste;
    public GameObject pistaVisual; // texto o UI que muestra la pista

    public void ComprarPista()
    {
        Debug.Log("Puntos actuales: " + PointsManager.Instance.puntos + " | Coste: " + coste);
        Debug.Log("Botón pulsado");

        if (PointsManager.Instance.SpendPoints(coste))
        {
            if (pistaVisual != null)
            {
                pistaVisual.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Pista visual no asignada en " + gameObject.name);
            }

            Destroy(gameObject);
        }
    }
}