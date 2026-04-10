using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioFondo : MonoBehaviour
{
    public Transform puntoDestino;

    public Transform nuevoYCerca;
    public Transform nuevoYLejos;

    public Vector2 nuevoMinBounds;
    public Vector2 nuevoMaxBounds;

    public void cambio()
    {
        Movimiento movimiento = FindFirstObjectByType<Movimiento>();

        StartCoroutine(CambioConFade(movimiento));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movimiento mov = other.GetComponent<Movimiento>();
        Camara cam = Camera.main.GetComponent<Camara>();

        if (mov != null)
        {
            StartCoroutine(CambioConFade(mov));
        }
    }

    private IEnumerator CambioConFade(Movimiento mov)
    {
        Camara cam = Camera.main.GetComponent<Camara>();

        //Fade out
        yield return StartCoroutine(fadeInOut.Instance.FadeOut(2));

        //Teletransportar
        mov.transform.position = puntoDestino.position;
        mov.cambioPerspectiva();
        mov.seguir = mov.transform.position;

        //Cambiar referencias de perspectiva
        mov.yCerca = nuevoYCerca;
        mov.yLejos = nuevoYLejos;

        //Mover camara
        cam.SetBounds(nuevoMinBounds, nuevoMaxBounds);

        yield return new WaitForSeconds(1f);
        //Fade in
        yield return StartCoroutine(fadeInOut.Instance.FadeIn(2));
    }
}
