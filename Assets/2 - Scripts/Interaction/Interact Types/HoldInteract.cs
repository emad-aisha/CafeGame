using UnityEngine;

public class HoldInteract : Interact {

    void Update() {
        HoldingUpdate();
    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        MenuManager.instance.EnableText(popupText);
        MenuManager.instance.ShowHoldBar(minWaitTimer * UIscale, maxWaitTimer * UIscale);
        hasInteracted = true;
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", interactType.ToString());
    }

    override protected bool Escape() { return !hasInteracted || holdAction == null; }

    override protected void ResetData() {
        base.ResetData();
        MenuManager.instance.DisableText();
    }

}
