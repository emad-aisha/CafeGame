using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouseInteract : Interact {
    InputAction mouseAction;

    void Update() {
        if (!hasInteracted && internalValue == 0) return;

        if (hasInteracted) {
            // update values
            UpdateMovingMouse();
            if (needsHeld) UpdateIsHolding();

            UpdateInternalValue();
            inRange = internalValue >= minValue && internalValue <= maxValue;
            if (inRange) hasInteracted = false;
        }
        else {
            if ((needsHeld && inRange) || isForgiving) {
                Debug.Log("Done");
                GameManager.instance.StartCamera();
                ResetInternalValue();
                hasInteracted = false;
            }
        }

    }


    public override void StartInteract(InteractionType interactedType) {
        if (interactType != interactedType) return;
        mouseAction = InputManager.instance.GetAction("Interaction", "MousePosition"); // mouse delta
        holdAction = InputManager.instance.GetAction("Interaction", interactedType.ToString()); // mouse delta
        hasInteracted = true;
        GameManager.instance.StopCamera();

        Debug.Log("start move mouse");
    }

    void UpdateMovingMouse() {
        if (mouseAction.ReadValue<Vector2>() != Vector2.zero) hasInteracted = true;
        else hasInteracted = false;
    }

}
