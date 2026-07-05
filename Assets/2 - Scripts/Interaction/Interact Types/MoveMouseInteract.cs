using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouseInteract : Interact {
    [SerializeField] bool shouldBeHeld;

    InputAction moveAction;
    Vector2 moveDelta;

    // TODO: set this up a bounds system ig

    void Update() {
        if (Escape()) return;
        if (!IsMoving()) return;

        HoldLogic();

        Timing timing = IncrementTimer(isHeld && internalTimer <= minWaitTimer);
        DoneUI(timing);

    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        MenuManager.instance.EnableText(popupText);
        hasInteracted = true;
        GameManager.instance.cameraController.Disable();

        moveAction = InputManager.instance.GetAction("Interaction", "MouseMovement");
        holdAction = InputManager.instance.GetAction("Interaction", _interactionType.ToString());
        isHeld = true;
    }

    override protected bool Escape() { return !hasInteracted || moveAction == null; }

    override protected Timing IncrementTimer(bool check) {
        if (!hasInteracted) return Timing.Null;

        // increments timer
        if (check) {
            internalTimer += Time.deltaTime;
        }
        else if (internalTimer > minWaitTimer) {
            return Timing.Done;
        }

        return Timing.NotDone;
    }

    bool IsMoving() {
        moveDelta = moveAction.ReadValue<Vector2>();
        if (moveDelta.x == 0 && moveDelta.y == 0) return false;
        return true;
    }

    override protected void HoldLogic() {
        if (shouldBeHeld) {
            if (holdAction == null) isHeld = false;
            else if (holdAction.IsPressed()) isHeld = true;
            else isHeld = false;
        }
        else {
            isHeld = true;
        }
    }

    override protected void DoneUI(Timing timing) {
        if (timing == Timing.Done) {
            ResetData();
            MenuManager.instance.DisableText();
            GameManager.instance.cameraController.Enable();
            StartCoroutine(MenuManager.instance.FlashInteract(doneCorrect, popupTime));
        }
    }


}
