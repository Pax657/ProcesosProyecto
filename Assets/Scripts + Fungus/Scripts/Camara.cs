using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Movimiento")]
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [Header("Límites opcionales")]
    public bool useBounds = true;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 1, -10); //Offset cámara - jugador

    void LateUpdate()
    {
        if (player == null) return;

        //Posición a mover
        Vector3 playerPos = player.position + offset;

        //Clampeamos entre los mínimos y máximos
        if (useBounds)
        {
            playerPos.x = Mathf.Clamp(playerPos.x, minBounds.x, maxBounds.x);
            playerPos.y = Mathf.Clamp(playerPos.y, minBounds.y, maxBounds.y);
        }

        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref velocity, smoothTime);
    }
}
