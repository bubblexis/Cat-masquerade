using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class uiButton : MonoBehaviour
{
    bool canClick = false;

    void Start()
    {
        StartCoroutine(EnableClickAfterDelay());
    }

    IEnumerator EnableClickAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); // 200ms delay
        canClick = true;
    }

    public void press()
    {
        if (!canClick) return;

        GlobalValues.lives = 3;
        print("Button pressed");
        SceneManager.LoadScene(1);
    }
}
