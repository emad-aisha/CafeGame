using UnityEngine;
using UnityEngine.InputSystem;

public class BlenderButton : Interactable {
    [SerializeField] Blender blenderInteract;
    [SerializeField] float blenderTimer;
    float internalTimer = 0;

    bool activated = false;

    void Update() {
        if (activated && internalTimer <= blenderTimer) {
            internalTimer += Time.deltaTime;
        }

        if (internalTimer >= blenderTimer) {
            Debug.Log("Finished Blending");
        }

        if (internalTimer >= blenderTimer && !activated) {
            Debug.Log("Stopped Blending");
            internalTimer = 0;
        }

    }

    public override void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;
        activated = !activated;

        if (activated) Debug.Log("Started Blending");
    }

    public override bool CanInteract(InputAction interactType) {
        if (!base.CanInteract(interactType)) return false;
        if (!blenderInteract.HasItem()) return false;
        return true;
    }
}
