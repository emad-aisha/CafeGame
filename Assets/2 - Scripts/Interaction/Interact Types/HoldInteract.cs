using UnityEngine;

public class HoldInteract : Interact {
    [SerializeField, Range(200, 400)] float UIscale;

    [SerializeField, Range(1.2f, 1.8f)] float minHold;
    [SerializeField, Range(0, 0.5f)] float maxHold;



    void Update() {
        if (Escape()) return;

        HoldLogic();

        if (isHeld) {
            internalTimer += Time.deltaTime;
            MenuManager.instance.UpdateHoldBar(internalTimer * UIscale);
        }
        else {
            if (internalTimer < minHold) {
                StartCoroutine(MenuManager.instance.FlashInteract(doneEarly));
            }
            else if (internalTimer > maxHold + minHold) {
                StartCoroutine(MenuManager.instance.FlashInteract(doneLate));
            }
            else {
                StartCoroutine(MenuManager.instance.FlashInteract(doneCorrect));
            }

            ResetData();
            MenuManager.instance.HideHoldBar();
        }

    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        MenuManager.instance.EnableText(popupText);
        MenuManager.instance.ShowHoldBar(minHold * UIscale, maxHold * UIscale);
        hasInteracted = true;
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", interactType.ToString());
    }

    override protected bool Escape() { return !hasInteracted || holdAction == null; }

    void ResetData() {
        MenuManager.instance.DisableText();
        internalTimer = 0;

        isHeld = false;
        hasInteracted = false;

        holdAction = null;
    }
}
