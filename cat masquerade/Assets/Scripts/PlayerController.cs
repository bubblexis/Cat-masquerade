using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Input")]
    public InputActionAsset actions;
    private InputAction moveAction;
    private InputAction rotationAction;
    private InputActionMap moveMap;

    [Header("Movement Settings")]
     public float speed;
     public float rotationSpeed;

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
        rotationAction = moveMap.FindAction("Rotation");
        if (rotationAction == null)
        {
            Debug.LogError("Rotation action not found");
        }
    }

    void movementFunction()
    {
     if (moveAction == null) return;

      Vector2 input = moveAction.ReadValue<Vector2>();
     Vector3 move = transform.forward * input.y; // forward/backward
     controller.Move(move * speed * Time.deltaTime);
    }

    void rotationFunction()
    {
        if (rotationAction == null) return;

        Vector2 input = rotationAction.ReadValue<Vector2>();
        float rotationAmount = input.x;

        if (Mathf.Abs(rotationAmount) > 0.1f)
        {
            transform.Rotate(Vector3.up, rotationAmount * rotationSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
      movementFunction();
      rotationFunction();
    }

}