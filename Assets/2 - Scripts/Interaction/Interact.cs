using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interact : MonoBehaviour {
    [SerializeField] protected InteractionType interactType;
    [SerializeField] protected bool needsHeld;
    protected InputAction holdAction;

    [Header("")]

    [SerializeField] protected float minValue;
    [SerializeField] protected float maxValue;
    [SerializeField, Range(1, 2)] float updateScale = 1;
    protected float internalValue;

    protected bool hasInteracted; // also double as an "isHeld" bool
    protected bool inRange;

    // shared behavior
    public abstract void StartInteract(InteractionType interactedType);

    protected void UpdateInternalValue() {
        internalValue += Time.deltaTime * updateScale;
    }
    protected void ResetInternalValue() {
        internalValue = 0;
    }

    protected void ResetHoldingValue() {
        hasInteracted = false;
    }
    protected void UpdateIsHolding() {
        if (holdAction != null) hasInteracted = holdAction.IsPressed();
        else hasInteracted = false;
    }

    protected bool IsHolding() {
        return hasInteracted;
    }


}
