using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Interact : MonoBehaviour {
    [SerializeField] protected string interactPopup;
    [SerializeField, Range(0.5f, 2)] protected float popupTime = 0.75f; // TODO: make this dynamically change with the size of a string

    [Header("")]
    [SerializeField] protected InteractionType interactType;
    [SerializeField] protected bool isForgiving;
    [SerializeField] protected bool needsHeld;
    protected InputAction holdAction;

    [Header("")]
    [SerializeField, Range(0, 1)] protected float minValue;
    [SerializeField, Range(0, 1)] protected float maxValue;
    [SerializeField, Range(0.1f, 1)] float updateScale = 0.5f; // TODO: maybe change into preset speeds for ease of use
    protected float internalValue;

    protected bool hasInteracted; // also double as an "isHeld" bool
    protected bool inRange;

    [HideInInspector] public UnityEvent startUI;

    void Start() {
        FlashUI flashUi = GetComponentInChildren<FlashUI>();
        startUI = new UnityEvent();

        startUI.AddListener(flashUi.Begin);
    }

    // shared behavior
    public abstract void StartInteract(InteractionType interactedType);
    protected void Begin() {
        hasInteracted = true;
        UpdateInternalValue();
        FlashUI();
    }
    protected void FlashUI() {
        startUI.Invoke();
    }

    protected void UpdateInternalValue() {
        internalValue += Time.deltaTime * updateScale;
    }
    protected void ResetInternalValues() {
        hasInteracted = false;
        inRange = false;
        internalValue = 0;
    }

    protected void ResetHoldingValue() {
        hasInteracted = false;
    }
    virtual protected void UpdateIsHolding() {
        if (holdAction != null) hasInteracted = holdAction.IsPressed();
        else hasInteracted = false;
    }

    protected bool IsHolding() {
        return hasInteracted;
    }

    // sharing personalData;
    public float GetInternalValue() { return internalValue; }
    public float GetMinValue() { return minValue; }
    public float GetMaxValue() { return maxValue; }

    public bool GetHasInteracted() { return hasInteracted; }
    public string GetInteractPopup() { return interactPopup; }
    public float GetPopupTime() { return popupTime; }

}
