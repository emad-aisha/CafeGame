using UnityEngine;

public class WaitInteract : Interact {
    [SerializeField] float waitTimer;

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

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartCoroutine(MenuManager.instance.FlashInteract(popupText, popupTime));
        Debug.Log("Started Waiting");
        hasInteracted = true;
    }

}
