using UnityEngine;

public class Toggle : NeededType, IInteractable {
    [SerializeField] InteractText toggleOn;
    [SerializeField] InteractText toggleOff;
    [SerializeField] InteractText finalResult;

    bool toggle;

    [SerializeField] float minToggleTimer;
    [SerializeField] float maxToggleTimer;
    float internalTimer;

    // TODO: right now it ends at the time
    //          change to make specific

    // TODO: make better lmao
    public void Update() {
        if (hasInteracted) {
            if (toggle) {
                internalTimer += Time.deltaTime;
                Debug.Log("timer: " + internalTimer);
            }
            else {
                if (internalTimer < minToggleTimer) {
                    Debug.Log("too little");
                }
                else if (internalTimer > maxToggleTimer) {
                    Debug.Log("too much");
                }
                else { // perfect
                    StartCoroutine(MenuManager.instance.FlashInteract(finalResult));
                }
                hasInteracted = false;
                internalTimer = 0;
            }
        }
        else {

        }
    }

    public void Interact(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        hasInteracted = true;
        toggle = !toggle;
        if (toggle) StartCoroutine(MenuManager.instance.FlashInteract(toggleOn));
        else StartCoroutine(MenuManager.instance.FlashInteract(toggleOff));
    }

    public bool Escape() { return true; }


}
