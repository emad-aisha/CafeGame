using UnityEngine;

public class ToggleInteract : Interact {

    void Update() {
        if (!hasInteracted && (internalValue == 0)) return;

        if (IsHolding()) {
            UpdateInternalValue();
            inRange = internalValue >= minValue && internalValue <= maxValue;
        }
        else if (!hasInteracted) {
            if (inRange) {
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
        hasInteracted = !hasInteracted;
        Debug.Log("toggle interact");
    }

}
