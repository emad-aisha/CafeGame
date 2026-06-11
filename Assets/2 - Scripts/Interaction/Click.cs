using UnityEngine;

public class Click : NeededType, IInteractable {
    // TODO: needs a Signal for other Interactions

    void Update() {
        if (hasInteracted) hasInteracted = false;
    }

    public override void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        hasInteracted = true;
        Debug.Log("Click");
    }
}
