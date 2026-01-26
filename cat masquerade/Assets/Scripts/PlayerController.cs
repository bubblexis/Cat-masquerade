using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset actions;
    private InputAction moveAction;
    private InputActionMap moveMap;

    [Header("Movement Settings")]
    public float speed = 5f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (actions == null)
        {
            Debug.LogError("Actions asset not assigned");
            return;
        }

        moveMap = actions.FindActionMap("move");
        if (moveMap == null)
        {
            Debug.LogError("Move action map not found!");
            return;
        }

        moveAction = moveMap.FindAction("Movement");
        if (moveAction == null)
        {
            Debug.LogError("Movement action not found");
        }
    }



    private void Update()
    {
        if (moveAction == null) return;

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y);
        if (move.magnitude > 0.1f)
        {
            controller.Move(move.normalized * speed * Time.deltaTime);
        }
    }
}
