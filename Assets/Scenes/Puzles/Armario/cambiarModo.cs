using UnityEngine;

public class cambiarModo : MonoBehaviour
{
    public Canvas modoNormal;
    public Canvas modoFacil;

    private void Start()
    {
        modoNormal.gameObject.SetActive(false);
        modoFacil.gameObject.SetActive(false);
    }

    public void activarFacil()
    {
        modoNormal.gameObject.SetActive(false);
        modoFacil.gameObject.SetActive(true);
    }

    public void activarNormal()
    {
        modoNormal.gameObject.SetActive(true);
        modoFacil.gameObject.SetActive(false);
    }

    public void puzzleTerminar()
    {
        modoNormal.gameObject.SetActive(false);
        modoFacil.gameObject.SetActive(false);
    }
}
