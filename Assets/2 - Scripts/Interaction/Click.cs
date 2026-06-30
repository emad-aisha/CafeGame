using UnityEngine;

public class Click : Interacter {

    void Update() {
        if (Escape()) return;
        hasInteracted = false;
    }

    override public void Interact(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartCoroutine(MenuManager.instance.FlashInteract(interactText));
        hasInteracted = true;
    }

    override public bool Escape() { return !hasInteracted; }
}
