using UnityEngine;

public class Wait : Interacter {
    [SerializeField] float waitTimer;
    float internalTimer;

    void Update() {
        if (Escape()) return;

        if (internalTimer <= waitTimer) {
            internalTimer += Time.deltaTime;
        }
        else {
            Debug.Log("Finished Waiting");
            internalTimer = 0;
            hasInteracted = false;
        }
    }

    override public void Interact(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartCoroutine(MenuManager.instance.FlashInteract(interactText));
        Debug.Log("Started Waiting");
        hasInteracted = true;
    }

    override public bool Escape() { return !hasInteracted; }
}
