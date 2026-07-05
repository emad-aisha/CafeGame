using UnityEngine;

public class ClickInteract : Interact {

    void Update() {
        if (Escape()) return;
        hasInteracted = false;
    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartUI();
        hasInteracted = true;
    }

}
