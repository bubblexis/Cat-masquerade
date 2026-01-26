using UnityEngine;
using UnityEngine.EventSystems; // required for UI events
using UnityEngine.SceneManagement;
public class TMPClickHandler : MonoBehaviour, IPointerClickHandler
{
    // PLACEHOLDER SCRIPT REALLY BAD BC UNITYS BEING WEIRD
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("UI object clicked!");
        OnClicked();
    }

    private void OnClicked()
    {
         SceneManager.LoadScene("Level1");
        Debug.Log("Do something on click!");
    }
}
