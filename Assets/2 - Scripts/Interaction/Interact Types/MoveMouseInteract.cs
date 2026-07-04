using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouseInteract : Interact
{
    [SerializeField] float moveTimer;

    [SerializeField] bool shouldBeHeld;

    InputAction moveAction;
    Vector2 moveVector;

    // TODO: set this up a bounds system ig

    void Update()
    {
        if (Escape()) return;
        if (!IsMoving()) return;

        HoldLogic();

        if (internalTimer <= moveTimer && isHeld)
        {
            internalTimer += Time.deltaTime;
            Debug.Log("Moving...");
        }
        else if (internalTimer > moveTimer)
        {
            Debug.Log("Finished Moving");
            MenuManager.instance.DisableText();
            GameManager.instance.cameraController.Enable();
            internalTimer = 0;
            hasInteracted = false;
            isHeld = false;
        }

    }

    override public void Act(InteractionType _interactionType)
    {
        if (!InteractTypeCheck(_interactionType)) return;
        MenuManager.instance.EnableText(popupText);
        Debug.Log("Move Start");
        hasInteracted = true;
        GameManager.instance.cameraController.Disable();

        moveAction = InputManager.instance.GetAction("Interaction", "MouseMovement");
        holdAction = InputManager.instance.GetAction("Interaction", _interactionType.ToString());
        isHeld = true;
    }

    override protected bool Escape() { return !hasInteracted || moveAction == null; }

    bool IsMoving()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x == 0 && moveVector.y == 0) return false;
        return true;
    }

}
