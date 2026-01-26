using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameButton : MonoBehaviour
{
    void pressbutton()
    {
     SceneManager.LoadScene("Level1");
    }
}
