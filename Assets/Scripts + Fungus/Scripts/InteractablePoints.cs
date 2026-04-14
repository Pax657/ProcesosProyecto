using UnityEngine;

public class InteractablePoints : MonoBehaviour
{
    public int puntosQueDa = 50;
    private bool usado = false;

    public GameObject floatingTextPrefab;
    public Canvas canvas;


    private void OnMouseDown()
    {
        if (!usado)
        {
            PointsManager.Instance.AddPoints(puntosQueDa);
            usado = true;

            MostrarTextoFlotante();

            Debug.Log("Click en objeto: +" + puntosQueDa + " puntos");

        }
    }

    void MostrarTextoFlotante()
    {
        GameObject texto = Instantiate(floatingTextPrefab, canvas.transform);

        texto.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        texto.GetComponent<FloatingText>().SetText("+" + puntosQueDa);

        Debug.Log("Texto instanciado");
    }
}