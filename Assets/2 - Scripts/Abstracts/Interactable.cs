using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable {
    [SerializeField] InteractionType preferredInteractionType; // TODO: find a way to hide count in inspector

    public abstract void Interact(string interactType);

    protected virtual bool MatchType(string interactType) {
        if (interactType != preferredInteractionType.ToString()) {
            Debug.Log("Couldn't interact");
            return false;
        }
        return true;
    }

    protected abstract bool CanInteract(string interactType);
}
