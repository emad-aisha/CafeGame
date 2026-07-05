using UnityEngine;
using UnityEngine.InputSystem;

// TODO: re-organize this + children
// TODO: update inspector for EACH child
// TODO: make new interactText?
public abstract class Interact : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] protected float UIScale;

    [SerializeField] protected string popupText;
    [SerializeField] protected float popupTime;

    [SerializeField] protected string doneEarly;
    [SerializeField] protected string doneLate;
    [SerializeField] protected string doneCorrect;

    [Header("Interaction")]
    [SerializeField] protected InteractionType interactType; // TODO: make sure this only has certain types ^ do in a custom editor

    [SerializeField] protected float minWaitTimer;
    [SerializeField] protected float maxWaitTimer;
    protected float internalTimer;

    // internal interaction
    protected bool hasInteracted;
    protected InputAction holdAction;
    protected bool isHeld;

    // FUNCTIONS ========================================================================
    abstract public void Act(InteractionType _interactionType);

    protected bool HoldingUpdate(bool check) {
        if (Escape()) return false;
        HoldLogic(); // updates isHeld

        Timing timing = IncrementTimer(check);
        UpdateBar();
        DoneUI(timing);
        return true;
    }

    // CHECKS ========================================================================
    protected enum Timing { Early, Late, Done, NotDone, Wrong, Null };
    // TODO: rename?
    // check determines what increments the timer ("isHeld" for example)
    virtual protected Timing IncrementTimer(bool check) {
        if (!hasInteracted) return Timing.Null;

        // increments timer
        if (check) {
            internalTimer += Time.deltaTime;
            return Timing.NotDone;
        }
        else {
            Timing returnValue;
            if (internalTimer < minWaitTimer) returnValue = Timing.Early;
            else if (internalTimer > maxWaitTimer) returnValue = Timing.Late;
            else returnValue = Timing.Done;

            return returnValue;
        }
    }

    virtual protected void HoldLogic() {
        if (holdAction == null) isHeld = false;
        else if (holdAction.IsPressed()) isHeld = true;
        else isHeld = false;
    }

    virtual protected bool Escape() {
        return !hasInteracted;
    }

    protected bool InteractTypeCheck(InteractionType newInteractType) {
        return newInteractType == interactType;
    }

    // UI ========================================================================
    // TODO: do some conversion to fix how the bar gets updated
    // double check this lol
    virtual protected void StartUI() {
        StartCoroutine(MenuManager.instance.FlashInteract(popupText, popupTime));
    }

    virtual protected void UpdateBar() {
        MenuManager.instance.UpdateHoldBar(internalTimer * UIScale);
    }

    virtual protected void DoneUI(Timing timing) {
        if (timing == Timing.Early || timing == Timing.Late || timing == Timing.Done) {
            ResetData();
            MenuManager.instance.HideHoldBar();
        }

        switch (timing) {
            case Timing.Early: StartCoroutine(MenuManager.instance.FlashInteract(doneEarly, popupTime)); break;
            case Timing.Late: StartCoroutine(MenuManager.instance.FlashInteract(doneLate, popupTime)); break;
            case Timing.Done: StartCoroutine(MenuManager.instance.FlashInteract(doneCorrect, popupTime)); break;
            default: break;
        }
    }

    // MISC ========================================================================
    virtual protected void ResetData() {
        internalTimer = 0;
        hasInteracted = false;
        isHeld = false;
        holdAction = null;
    }



}
