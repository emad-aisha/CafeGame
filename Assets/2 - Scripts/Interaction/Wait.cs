using UnityEngine;

public class Wait : NeededType, IInteractable {
    [SerializeField] float waitTimer;
    float internalTimer;

    void Update() {
        if (!hasInteracted) return;

        if (internalTimer <= waitTimer) {
            internalTimer += Time.deltaTime;
        }
        else {
            Debug.Log("Finished Waiting");
            internalTimer = 0;
        }
    }

    public override void Interact(InteractionType _interactionType) {
        if (neededInteractionType != _interactionType) return;
        Debug.Log("Started Waiting");
        hasInteracted = true;
    }

}
