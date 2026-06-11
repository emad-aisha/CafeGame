using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour {
    [SerializeField] InteractionType preferredInteractionType;

    public abstract void Interact(InputAction interactType);

    // can change if interaction needs change (in children)
    public virtual bool CanInteract(InputAction interactType) {
        if (!MatchType(interactType.name)) return false;
        return true;
    }

    protected bool MatchType(string interactType) { return interactType == preferredInteractionType.ToString(); }

}
