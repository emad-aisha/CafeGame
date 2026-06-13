using UnityEngine;

// TODO: needs a Signal
public interface IInteractable {
    public void Interact(InteractionType _interactionType);

    public bool Escape();
}
