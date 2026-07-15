using UnityEngine;

public class HoldInteract : Interact {

    void Update() {
        if (!hasInteracted && internalValue == 0) return;

        // if stopped holding in this frame, check values
        if (IsHolding()) {
            UpdateIsHolding();

            UpdateInternalValue();
            inRange = internalValue >= minValue && internalValue <= maxValue;
        }
        else {
            if (!needsHeld || inRange || isForgiving) {
                Debug.Log("Done");
            }
            else if (internalValue < minValue) {
                Debug.Log("Early");
            }
            else if (internalValue > maxValue) {
                Debug.Log("Late");
            }

            ResetInternalValue();
        }

    }


    public override void StartInteract(InteractionType interactedType) {
        if (interactType != interactedType) return;
        hasInteracted = true;
        if (needsHeld) holdAction = InputManager.instance.GetAction("Interaction", interactType.ToString());
        Debug.Log("start interact");
    }

}
