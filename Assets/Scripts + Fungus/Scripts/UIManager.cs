using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI puntosText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePointsText(int puntos)
    {
        puntosText.text = "Puntos: " + puntos;
    }
}