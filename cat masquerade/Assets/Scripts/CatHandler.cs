using UnityEngine;
using UnityEngine.InputSystem; // for new Input System
using UnityEngine.SceneManagement;


public class CatHandler : MonoBehaviour
{
    public GameObject mask;             // the object to hover over
    public bool Target;
    public Texture2D hoverCursor;       // the cursor texture when hovering
    public Vector2 hotspot = Vector2.zero; // cursor hotspot
    private Texture2D defaultCursor;



  void OnMaskClicked()
   {
    if (Target == false)
    {
     print("Wrong!");
     GlobalValues.lives -= 1;  
     print("Current lives: " + GlobalValues.lives);

     SceneManager.LoadScene(SceneManager.GetActiveScene().name);

     print("Scene reloaded");
    
    
    }
    else
    {
     print("Correct! level up");

         if (SceneManager.GetActiveScene().buildIndex + 1 > 5)
        {
            print("Scene is already 5, you win");
            SceneManager.LoadScene(MainMenu)
            return;
        }

     print("Current level: " +  SceneManager.GetActiveScene().buildIndex + 1);

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);

    }
   }




    void Start()
    {
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
                Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        Debug.Log("clicked, take off mask");
                        OnMaskClicked();
                    }
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
