using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable {
    [SerializeField] string preferredInteractionType; // TODO: find a way to make this only use preset Options

    public abstract void Interact(string interactType);

    protected bool CanInteract(string interactType) {
        if (interactType != preferredInteractionType) {
            Debug.Log("Couldn't interact");
            return false;
        }
        return true;
    }
}
