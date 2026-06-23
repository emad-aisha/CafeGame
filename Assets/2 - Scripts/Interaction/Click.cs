using UnityEngine;

public class Click : NeededType, IInteractable {

    void Update() {
        if (Escape()) return;
        hasInteracted = false;
    }

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        StartCoroutine(MenuManager.instance.FlashInteract(interactText));
        hasInteracted = true;
        Debug.Log("Click");
    }

    public bool Escape() { return !hasInteracted; }
}
