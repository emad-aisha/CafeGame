using UnityEngine;

// TODO: re-organize this + children
public abstract class Interacter : MonoBehaviour {
    [SerializeField] protected InteractText interactText;
    [SerializeField] protected InteractionType interactType;

    [HideInInspector] public bool hasInteracted;

    abstract public void Interact(InteractionType _interactionType);

    abstract public bool Escape();

    protected bool InteractTypeCheck(InteractionType newInteractType) {
        return newInteractType == interactType;
    }


}
