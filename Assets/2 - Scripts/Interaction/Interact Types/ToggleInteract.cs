using UnityEngine;
using static MenuManagerHelper;

public class ToggleInteract : Interact {
    MenuManagerHelper helper;
    [SerializeField] GameObject holdBarObject;
    [SerializeField] GameObject greenRange;
    [SerializeField] GameObject holdRange;
    bool toggle;

    // TODO: right now it ends at the time
    //          change to make specific
    void Start() {
        helper = new();
        helper.SetHoldBarData(greenRange, holdRange);
    }

    // TODO: make better lmao
    public void Update() {
        HoldingUpdate(toggle);
    }

    override public void Act(InteractionType _interactionType) {
        if (!InteractTypeCheck(_interactionType)) return;
        hasInteracted = true;
        toggle = !toggle;
        if (toggle) StartUI();
    }

    // UI ========================================================================
    protected override void StartUI() {
        base.StartUI();

        helper.SetBarPosition(Type.Green, minWaitTimer * UIScale);
        helper.SetBarWidth(Type.Green, maxWaitTimer * UIScale);

        holdBarObject.SetActive(true);
    }

    override protected void UpdateBar() {
        helper.SetBarWidth(Type.Hold, internalTimer * UIScale);
    }

    override protected void DoneUI(Timing timing) {
        if (timing == Timing.Early || timing == Timing.Late || timing == Timing.Done) {
            ResetData();
            helper.SetBarPosition(Type.Green, 0, 0);
            helper.SetBarWidth(Type.Green, 0);
            helper.SetBarWidth(Type.Hold, 0);

            holdBarObject.SetActive(false);
        }

        switch (timing) {
            case Timing.Early: StartCoroutine(MenuManager.instance.FlashInteract(doneEarly, popupTime)); break;
            case Timing.Late: StartCoroutine(MenuManager.instance.FlashInteract(doneLate, popupTime)); break;
            case Timing.Done: StartCoroutine(MenuManager.instance.FlashInteract(doneCorrect, popupTime)); break;
            default: break;
        }
    }

}
