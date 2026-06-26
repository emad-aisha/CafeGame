using UnityEngine;
using UnityEngine.InputSystem;

public class Hold : NeededType, IInteractable {
    [SerializeField] float minHold;
    [SerializeField] float maxHold;

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
        }
        else {
            if (internalTimer < minHold) { // early
                StartCoroutine(MenuManager.instance.FlashInteract(endedWrong));
            }
            else if (internalTimer > maxHold) { // late
                StartCoroutine(MenuManager.instance.FlashInteract(endedWrong));
            }
            else { // just right
                StartCoroutine(MenuManager.instance.FlashInteract(ended));
            }

            ResetData();
        }

    }

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        MenuManager.instance.EnableText(interactText);
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
