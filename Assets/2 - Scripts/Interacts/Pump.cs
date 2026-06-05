using UnityEngine;
using UnityEngine.InputSystem;

public class Pump : Interactable {
    [SerializeField] float pumpTimer;
    float internalTimer = 0;

    InputAction action;
    bool isPumping;

    void Update() {
        if (action != null && action.IsPressed()) { isPumping = true; }
        else { isPumping = false; }

        if (isPumping) {
            internalTimer += Time.deltaTime; // TODO: make a tick system maybe

            if (internalTimer >= pumpTimer) {
                Debug.Log("Pumped");
                action = null;
                internalTimer = 0;
            }
        }
    }

    public override void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;
        action = interactType;
    }
}
