using UnityEngine;
using UnityEngine.InputSystem; // for new Input System

public class CatHandler : MonoBehaviour
{
    public GameObject mask;             // the object to hover over
    public Texture2D hoverCursor;       // the cursor texture when hovering
    public Vector2 hotspot = Vector2.zero; // cursor hotspot
    private Texture2D defaultCursor;

    void Start()
    {
        // Save default cursor (optional, you can also just set it to null)
        defaultCursor = null;
    }

    void Update()
    {
        if (Mouse.current == null) return; // safety for no mouse

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == mask.transform)
            {
                // Hovering → change cursor
                Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
            }
            else
            {
                // Not hovering → default cursor
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
