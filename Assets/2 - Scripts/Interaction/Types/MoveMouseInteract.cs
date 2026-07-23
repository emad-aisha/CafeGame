using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouseInteract : Interact {
    InputAction mouseAction;

    void Update() {
        if (!hasInteracted && internalValue == 0) return;

        // update values
        if (needsHeld) UpdateIsHolding();
        else UpdateMovingMouse();

        if (hasInteracted) {
            UpdateInternalValue();

            inRange = internalValue >= minValue && internalValue <= maxValue;
            if (inRange) hasInteracted = false;
        }
        else if ((needsHeld && inRange) || inRange) {
            Debug.Log("Done");
            GameManager.instance.StartCamera();
            ResetInternalValues();
            mouseAction = new InputAction("");
        }

    }


    public override void StartInteract(InteractionType interactedType) {
        if (interactType != interactedType) return;
        Debug.Log("mouseAction: " + mouseAction);
        if (mouseAction.name == "") Begin(); // to prevent bug

        // set actions
        mouseAction = InputManager.instance.GetAction("Interaction", "MouseMovement"); // mouse delta
        if (needsHeld) holdAction = InputManager.instance.GetAction("Interaction", interactedType.ToString()); // mouse delta

        GameManager.instance.StopCamera();
        Debug.Log("start move mouse");
    }

    void UpdateMovingMouse() {
        if (mouseAction.ReadValue<Vector2>() != Vector2.zero) hasInteracted = true;
        else hasInteracted = false;
    }

    override protected void UpdateIsHolding() {
        if (mouseAction.ReadValue<Vector2>() != Vector2.zero && holdAction != null) hasInteracted = holdAction.IsPressed();
        else hasInteracted = false;
    }


}
