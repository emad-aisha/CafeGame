using UnityEngine;
using UnityEngine.InputSystem;

public class Pour : Interactable {
    [SerializeField] float minRange;
    [SerializeField] float maxRange;
    bool isPouring;
    float internalValue;

    InputAction action;

    void Update() {
        if (action != null && action.IsPressed()) { isPouring = true; }
        else { isPouring = false; }

        if (isPouring) {
            internalValue += Time.deltaTime; // TODO: make a tick system maybe
        }
        else {
            if (internalValue >= minRange && internalValue <= maxRange) {
                Debug.Log("Finished Pouring");
                internalValue = 0;
                action = null;
            }
            else if (internalValue < minRange && internalValue > 0) {
                Debug.Log("Not Done Pouring");
            }
            else if (internalValue > maxRange) {
                Debug.Log("Over Filled");
                internalValue = 0;
                action = null;
            }
        }
    }

    public override void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;
        action = interactType;
    }

}
