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
    bool hasClicked = false;
    public int currentlevel;

     public Animator animator;

    public GameObject EarsAddon;
    public GameObject Hornsaddon;
    public GameObject WhiskersAddon;

   void RandomizeKitty()
    {
        /// in animationcontroller, make an int random 1-7
        /// 
        int randomValue = Random.Range(1, 8); // 1 to 7 inclusive
        animator.SetInteger("Color", randomValue);
    }
    void RandomizeAddons()
    {
        EarsAddon.SetActive(Random.value > 0.5f);
        Hornsaddon.SetActive(Random.value > 0.5f);
        WhiskersAddon.SetActive(Random.value > 0.5f);
    }

    void OnMaskClicked()
    {
        if (Target == false)
        {
            print("Wrong!");

            GlobalValues.lives -= 1;
            if (GlobalValues.lives <= 0)
            {
                print("You lose");
                SceneManager.LoadScene(sceneBuildIndex: 0);
                return;
            }
            else
            {
                print("Current lives: " + GlobalValues.lives);

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                print("Scene reloaded");

            }

        }
        else
        {
            print("Correct! level up");

            if (SceneManager.GetActiveScene().buildIndex + 1 > 5)
            {
                print("Scene is already 5, you win");
                SceneManager.LoadScene(sceneBuildIndex: 0); // MainMenu (Should be index 0)
                return;
            }

            print("Current level: " + (SceneManager.GetActiveScene().buildIndex + 1));

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);

        }
    }

    void Start()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex + 1;
         animator.SetInteger("Level", currentlevel);
        defaultCursor = null;
        RandomizeKitty();
        if (Target == false)
        {
            RandomizeAddons();
        }
    }

    void Update()
    {
        if (hasClicked) return;
        if (Mouse.current == null) return;

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
                    hasClicked = true;
                    Debug.Log("clicked, take off mask");
                    OnMaskClicked();
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

}
