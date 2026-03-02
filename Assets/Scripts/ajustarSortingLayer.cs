using UnityEngine;

public class ajustarSortingLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)transform.position.y * -10;
    }
}
