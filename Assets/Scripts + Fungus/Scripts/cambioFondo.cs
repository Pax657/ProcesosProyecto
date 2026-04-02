using UnityEngine;

public class cambioFondo : MonoBehaviour
{
    public Transform puntoDestino;

    public Transform nuevoYCerca;
    public Transform nuevoYLejos;

    public Vector2 nuevoMinBounds;
    public Vector2 nuevoMaxBounds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Movimiento mov = other.GetComponent<Movimiento>();
        Camara cam = Camera.main.GetComponent<Camara>();

        if (mov != null)
        {
            //Teletransportar
            mov.transform.position = puntoDestino.position;
            mov.cambioPerspectiva();
            mov.seguir = mov.transform.position;

            //Cambiar referencias de perspectiva
            mov.yCerca = nuevoYCerca;
            mov.yLejos = nuevoYLejos;

            //Mover camara
            cam.SetBounds(nuevoMinBounds, nuevoMaxBounds);
        }
    }
}
