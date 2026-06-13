using UnityEngine;
using UnityEngine.InputSystem;

public class Hold : NeededType, IInteractable {
    [SerializeField] float holdTimer;

    InputAction holdAction;
    float internalTimer;
    bool isHeld;

    void Update() {
        if (Escape()) return;
        
        HoldLogic();

        if (isHeld) {
            if (internalTimer <= holdTimer) {
                Debug.Log("Holding...");
                internalTimer += Time.deltaTime;
            }
            else {
                Debug.Log("Finished Holding");
                ResetData();
            }
        }
        else {
            Debug.Log("Ended Early");
            ResetData();
        }

    }

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        hasInteracted = true;
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", neededInteractionType.ToString());

        Debug.Log("Start Hold");
    }

    public bool Escape() { return !hasInteracted || holdAction == null; }

    void HoldLogic() {
        if (holdAction.IsPressed()) { isHeld = true; }
        else { isHeld = false; }
    }

    void ResetData() {
        internalTimer = 0;

        isHeld = false;
        hasInteracted = false;

        holdAction = null;
    }
}
