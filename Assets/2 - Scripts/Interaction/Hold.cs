using UnityEngine;
using UnityEngine.InputSystem;

public class Hold : NeededType, IInteractable {
    [SerializeField, Range(200, 400)] float UIscale;

    [SerializeField, Range(1.2f, 1.8f)] float minHold;
    [SerializeField, Range(0, 0.5f)] float maxHold;

    [SerializeField] InteractText endedWrong;
    [SerializeField] InteractText ended;

    InputAction holdAction;
    float internalTimer;
    bool isHeld;

    void Update() {
        if (Escape()) return;

        HoldLogic();

        if (isHeld) {
            internalTimer += Time.deltaTime;
            MenuManager.instance.UpdateHoldBar(internalTimer * UIscale);
        }
        else {
            if (internalTimer < minHold) { // early
                StartCoroutine(MenuManager.instance.FlashInteract(endedWrong));
            }
            else if (internalTimer > maxHold + minHold) { // late
                StartCoroutine(MenuManager.instance.FlashInteract(endedWrong));
            }
            else { // just right
                StartCoroutine(MenuManager.instance.FlashInteract(ended));
            }

            ResetData();
            MenuManager.instance.HideHoldBar();
        }

    }

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        MenuManager.instance.EnableText(interactText);
        MenuManager.instance.ShowHoldBar(minHold * UIscale, maxHold * UIscale);
        hasInteracted = true;
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", neededInteractionType.ToString());
    }

    public bool Escape() { return !hasInteracted || holdAction == null; }

    void HoldLogic() {
        if (holdAction.IsPressed()) { isHeld = true; }
        else { isHeld = false; }
    }

    void ResetData() {
        MenuManager.instance.DisableText();
        internalTimer = 0;

        isHeld = false;
        hasInteracted = false;

        holdAction = null;
    }
}
