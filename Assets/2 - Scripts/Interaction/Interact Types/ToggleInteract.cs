using UnityEngine;

public class ToggleInteract : Interact {
    [SerializeField] string toggleOn;
    [SerializeField] string toggleOff;
    [SerializeField] string finalResult;

    bool toggle;

    [SerializeField] float minToggleTimer;
    [SerializeField] float maxToggleTimer;

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
                    StartCoroutine(MenuManager.instance.FlashInteract(finalResult, popupTime));
                }
                hasInteracted = false;
                internalTimer = 0;
            }
        }
        else {

        }
    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        hasInteracted = true;
        toggle = !toggle;
        if (toggle) StartCoroutine(MenuManager.instance.FlashInteract(toggleOn, 0.5f));
        else StartCoroutine(MenuManager.instance.FlashInteract(toggleOff, 0.5f));
    }


}
