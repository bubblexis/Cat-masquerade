using UnityEngine;

public class CatHandler : MonoBehaviour
{
    public GameObject mask;
     private Renderer rend;
    private Color originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == mask.transform)
            {
                // Mouse is hovering over this object
                rend.material.color = Color.yellow;
            }
            else
            {
                rend.material.color = originalColor;
            }
        }
        else
        {
            rend.material.color = originalColor;
        }
    }
}
