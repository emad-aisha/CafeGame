using UnityEngine;

public class ClickInteract : Interact {

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartUI();
        hasInteracted = true;
    }

}
