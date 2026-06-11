using UnityEngine;

public class Click : MonoBehaviour, IInteractable {
    [SerializeField] InteractionType neededInteractionType;
    // TODO: needs a Signal for other Interactions

    public void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;

        Debug.Log("Click");
    }
}
