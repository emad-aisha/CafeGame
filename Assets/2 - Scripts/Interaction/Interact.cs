using UnityEngine;
using UnityEngine.InputSystem;

// TODO: re-organize this + children
public abstract class Interact : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] protected InteractText popupText;
    [SerializeField] protected float popupTime;

    [SerializeField] protected InteractText doneEarly;
    [SerializeField] protected InteractText doneLate;
    [SerializeField] protected InteractText doneCorrect;

    [Header("Interaction")]
    [SerializeField] protected InteractionType interactType; // TODO: make sure this only has certain types ^ do in a custom editor

    [SerializeField] protected float waitTimer;
    protected float internalTimer;

    // internal interaction
    protected bool hasInteracted;
    protected InputAction holdAction;
    protected bool isHeld;

    // FUNCTIONS ========================================================================
    abstract public void Act(InteractionType _interactionType);

    void Update() {
        if (Escape()) return;
        IncrementTimer();
        UpdateBar();
    }

    // CHECKS ========================================================================
    // TODO: rename?
    virtual protected bool IncrementTimer() {
        if (!hasInteracted) return false;

        if (internalTimer < waitTimer) {
            internalTimer += Time.deltaTime;
            return false;
        }
        else {
            internalTimer = 0;
            hasInteracted = false;
            return true;
        }
    }

    virtual protected void HoldLogic() {
        if (holdAction.IsPressed()) { isHeld = true; }
        else { isHeld = false; }
    }

    virtual protected bool Escape() {
        return !hasInteracted;
    }

    protected bool InteractTypeCheck(InteractionType newInteractType) {
        return newInteractType == interactType;
    }

    // UI ========================================================================
    // TODO: do some conversion to fix how the bar gets updated
    virtual protected void StartUI() {
        StartCoroutine(MenuManager.instance.FlashInteract(popupText, popupTime));
    }

    virtual protected void UpdateBar() {
        MenuManager.instance.UpdateHoldBar(internalTimer);
    }

}
