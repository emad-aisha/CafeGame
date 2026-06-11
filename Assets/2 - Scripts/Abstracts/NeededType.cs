using UnityEngine;

public class NeededType : MonoBehaviour, IInteractable {
    [SerializeField] protected InteractionType neededInteractionType;
    [HideInInspector] public bool hasInteracted;

    public virtual void Interact(InteractionType _interactionType) { }

}
