using UnityEngine;

public class BlenderButton : Interactable {
    [SerializeField] float blenderTimer;
    float internalTimer = 0;

    bool activated = false;
    [SerializeField] bool hasItem; // TODO: set this from smth else / Serialized is TEMP

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
        bool finalResult = base.CanInteract(interactType);

        if (!hasItem) return false;
        return finalResult;
    }
}
