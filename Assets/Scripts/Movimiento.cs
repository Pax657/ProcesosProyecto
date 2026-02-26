using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Vector2 seguir;

    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seguir = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
