using System.Collections;
using System.Collections.Generic;
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

    public int Randomizedmask;

    public List<AudioClip> catClips = new();
    public List<AudioClip> winLoseClips = new();
    public AudioSource source;


    void RandomizeKitty()
    {
        /// in animationcontroller, make an int random 1-7
        /// 
        int randomValue = Random.Range(1, 8); // 1 to 7 inclusive
        animator.SetInteger("Color", randomValue);
    }

    void PlayrandomLoopedpose()
    {
        int randomValue = Random.Range(1, 5); // 1 to 4 inclusive
        animator.SetInteger("Pose", randomValue);
    }


    void OnMaskClicked()
    {
        StartCoroutine(WinLoseCondition());

    }

    IEnumerator WinLoseCondition()
    {
        //play sound when clicking on a cat, and before changing scenes
        source.clip = catClips[Random.Range(0, catClips.Count)];
        source.Play();

        yield return new WaitForSeconds(1);

        if (Target == false)
        {
            print("Wrong!");

            source.clip = winLoseClips[0];
            source.Play();

            yield return new WaitForSeconds(1);

            source.Stop();

            GlobalValues.lives -= 1;
            if (GlobalValues.lives <= 0)
            {
                print("You lose");
                SceneManager.LoadScene(sceneBuildIndex: 0);
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

            source.clip = winLoseClips[1];
            source.Play();

            yield return new WaitForSeconds(1);

            source.Stop();

            if (SceneManager.GetActiveScene().buildIndex + 1 > 5)
            {
                print("Scene is already 5, you win");
                SceneManager.LoadScene(sceneBuildIndex: 0); // MainMenu (Should be index 0)
            }
            else
            {
                print("Current level: " + (SceneManager.GetActiveScene().buildIndex + 1));

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }

    void Start()
    {
        currentlevel = SceneManager.GetActiveScene().buildIndex;
        print("Current level at start: " + currentlevel);
        animator.SetInteger("Level", currentlevel);
        defaultCursor = null;
        RandomizeKitty();
        PlayrandomLoopedpose();
        if (Target == false)
        {
            EarsAddon.SetActive(Random.value > 0.5f);
            Hornsaddon.SetActive(Random.value > 0.5f);
            WhiskersAddon.SetActive(Random.value > 0.5f);

            if (currentlevel == 1)
            { //// evil preventer level 1 ...


                Randomizedmask = Random.Range(1, 6);
                animator.SetInteger("RandomMask", Randomizedmask);

                print("Randomized mask value: " + Randomizedmask);

                if (EarsAddon.activeSelf == false && Hornsaddon.activeSelf == false && WhiskersAddon.activeSelf == false && Randomizedmask == 1)
                {
                    EarsAddon.SetActive(true);
                }
            }
            if (currentlevel == 2)
            { //// evil preventer level 2 ...


                Randomizedmask = Random.Range(1, 7);
                animator.SetInteger("RandomMask", Randomizedmask);

                if (EarsAddon.activeSelf == true && Hornsaddon.activeSelf == true && WhiskersAddon.activeSelf == true && Randomizedmask == 1)
                {
                    EarsAddon.SetActive(false);
                }
            }
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
