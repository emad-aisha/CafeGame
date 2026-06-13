using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouse : NeededType, IInteractable {
    [SerializeField] bool shouldBeHeld;
    InputAction moveAction;
    Vector2 moveVector;

    InputAction holdAction;
    bool isHeld;

    [SerializeField] float moveTimer;
    float internalTimer;


    void Update() {
        if (!hasInteracted) return;
        if (moveAction == null) return;

        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x == 0 && moveVector.y == 0) return;

        if (shouldBeHeld) { isHeld = holdAction.IsPressed(); }
        else isHeld = true;

        if (internalTimer <= moveTimer && isHeld) {
            internalTimer += Time.deltaTime;
            Debug.Log("Moving...");
        }
        else if (internalTimer <= moveTimer && !isHeld) {
            Debug.Log("Ended Moving Early");
            internalTimer = 0;
            hasInteracted = false;
            GameManager.instance.cameraController.Enable();
            isHeld = false;
        }
        else {
            Debug.Log("Finished Moving");
            internalTimer = 0;
            hasInteracted = false;
            GameManager.instance.cameraController.Enable();
            isHeld = false;
        }

    }

    public override void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        Debug.Log("Move Start");
        hasInteracted = true;
        GameManager.instance.cameraController.Disable();
        moveAction = InputManager.instance.GetAction("Interaction", InteractionType.MouseMovement.ToString());

        holdAction = InputManager.instance.GetAction("Interaction", _interactionType.ToString());
        isHeld = true;
    }
}
