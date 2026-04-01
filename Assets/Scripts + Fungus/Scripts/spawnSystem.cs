using UnityEngine;

public class spawnSystem : MonoBehaviour
{

    public static Transform nextSpawnPoint;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (gameManager.nextSpawnPoint != null)
        {
            Vector3 spawnPos = gameManager.nextSpawnPoint.position;
            spawnPos.z = transform.position.z;

            transform.position = spawnPos;

            // sincronizar con movimiento
            Movimiento mov = GetComponent<Movimiento>();
            if (mov != null)
            {
                mov.seguir = transform.position;
            }

            // limpiar
            gameManager.nextSpawnPoint = null;
        }
    }
}