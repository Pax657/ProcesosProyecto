using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private Texture2D currentCursor;
    private Vector2 hotspot;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void SetCursor(Texture2D tex, Vector2 newHotspot)
    {
        if (currentCursor == tex) return;

        Cursor.SetCursor(tex, newHotspot, CursorMode.Auto);
        currentCursor = tex;
        hotspot = newHotspot;
    }

    public Texture2D GetCurrentCursor()
    {
        return currentCursor;
    }
}