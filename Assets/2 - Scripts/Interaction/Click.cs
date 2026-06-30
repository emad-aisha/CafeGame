using UnityEngine;

public class Click : NeededType, IInteractable {

    void Update() {
        if (Escape()) return;
        hasInteracted = false;
    }

    public void Interact(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartCoroutine(MenuManager.instance.FlashInteract(interactText));
        hasInteracted = true;
    }

    public bool Escape() { return !hasInteracted; }
}
