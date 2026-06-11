using UnityEngine;
using UnityEngine.InputSystem;

public class Hold : MonoBehaviour, IInteractable {
    [SerializeField] InteractionType neededInteractionType;
    [SerializeField] float holdTimer;

    InputAction holdAction;
    float internalTimer;
    bool isHeld;

    void Update() {
        if (holdAction == null) return;

        // holding logic
        if (holdAction.IsPressed()) { isHeld = true; }
        else { isHeld = false; }


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
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", neededInteractionType.ToString());

        Debug.Log("Start Hold");
    }


    void ResetData() {
        internalTimer = 0;
        isHeld = false;
        holdAction = null;
    }
}
