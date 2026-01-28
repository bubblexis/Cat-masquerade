using UnityEngine;
using UnityEngine.SceneManagement;

public class uiButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void press()
    {
        print("Button pressed");
     SceneManager.LoadScene(sceneBuildIndex: 1);
     GlobalValues.lives = 3;  
    }
}
