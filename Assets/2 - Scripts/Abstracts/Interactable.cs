using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    [SerializeField] InteractionType preferredInteractionType;

    public abstract void Interact(string interactType);

    // can change if interaction needs change (in children)
    public virtual bool CanInteract(string interactType) {
        if (!MatchType(interactType)) return false;
        return true;
    }

    bool MatchType(string interactType) {
        if (interactType != preferredInteractionType.ToString()) {
            return false;
        }
        return true;
    }

}
