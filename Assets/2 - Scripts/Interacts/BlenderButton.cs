using UnityEngine;

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

    public override void Interact(string interactType) {
        if (!CanInteract(interactType)) return;
        activated = !activated;

        if (activated) Debug.Log("Started Blending");
    }

    protected override bool CanInteract(string interactType) {
        bool finalResult = MatchType(interactType);

        if (!blenderInteract.HasItem()) return false;
        return finalResult;
    }
}
