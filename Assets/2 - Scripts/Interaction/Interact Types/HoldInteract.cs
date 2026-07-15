using UnityEngine;

public class HoldInteract : Interact {

    void Update() {
        HoldingUpdate(isHeld);
    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        StartUI();
        hasInteracted = true;
        isHeld = true;
        holdAction = InputManager.instance.GetAction("Interaction", interactType.ToString());
    }

    override protected bool Escape() { return !hasInteracted || holdAction == null; }

    override protected void ResetData() {
        base.ResetData();
        MenuManager.instance.DisableText();
    }

    override protected void StartUI() {
        MenuManager.instance.EnableText(popupText);
        MenuManager.instance.ShowHoldBar(minWaitTimer * UIScale, maxWaitTimer);
    }

}
