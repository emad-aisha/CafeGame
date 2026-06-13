using UnityEngine;

public class Wait : NeededType, IInteractable {
    [SerializeField] float waitTimer;
    float internalTimer;

    void Update() {
        if (Escape()) return;

        if (internalTimer <= waitTimer) {
            internalTimer += Time.deltaTime;
        }
        else {
            Debug.Log("Finished Waiting");
            internalTimer = 0;
            hasInteracted = false;
        }
    }

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType || hasInteracted) return;
        Debug.Log("Started Waiting");
        hasInteracted = true;
    }

    public bool Escape() { return !hasInteracted; }
}
