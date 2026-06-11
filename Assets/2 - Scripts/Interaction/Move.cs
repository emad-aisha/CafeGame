using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour, IInteractable
{
    [SerializeField] Click initalInteract;
    bool canRun;

    InputAction moveAction;
    Vector2 moveVector;

    [SerializeField] float moveTimer;
    float internalTimer;


    void Update()
    {
        if (!canRun) return;
        if (moveAction == null) return;

        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x == 0 && moveVector.y == 0) return;

        if (internalTimer <= moveTimer)
        {
            internalTimer += Time.deltaTime;
            Debug.Log("Moving...");
        }
        else
        {
            Debug.Log("Finished Moving");
            internalTimer = 0;
            canRun = false;
            GameManager.instance.cameraController.Enable();
        }

    }

    public void Interact(InteractionType _interactionType)
    {
        initalInteract.Interact(_interactionType);

        if (initalInteract.hasInteracted)
        {
            Debug.Log("Move Start");
            GameManager.instance.cameraController.Disable();
            moveAction = InputManager.instance.GetAction("Interaction", InteractionType.MouseMovement.ToString());
            canRun = true;
        }
    }
}
