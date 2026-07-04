using UnityEngine;
using UnityEngine.InputSystem;

// TODO: move to somewhere that makes sense
public enum InteractionType {
    [InspectorName("Left Click")] LeftClick,
    [InspectorName("Right Click")] RightClick,
    Primary,
    Secondary,
    [InspectorName(null)] Count
};

public class InteractionController : InputSystems {
    [SerializeField] int distance;
    [SerializeField] float interactTimer;
    float internalInteractTimer;

    [SerializeField] LayerMask ignoreLayer;

    InputAction[] interactActions;
    int totalTypes;

    void Start() {
        SetTotalTypes();
        SetInteractActions();
    }

    void Update() {
        InteractCheck();
    }

    void InteractCheck() {
        if (internalInteractTimer <= 0) {
            if (GetInputAction() == null) return;
            internalInteractTimer = interactTimer;
            Act();
        }
        else {
            internalInteractTimer -= Time.deltaTime;
        }
    }

    void Act() {
        RaycastHit hit;

        bool hitInteractable = Physics.Raycast(GameManager.instance.mainCamera.transform.position, GameManager.instance.mainCamera.transform.forward,
            out hit, distance, ~ignoreLayer);

        // TODO: implement
        //  Pickup if click was pressed
        //  Interact is e/f was pressed (depeneds on the interact item)
        //  ETC....

        if (hitInteractable) {
            Pickup pickup = hit.collider.GetComponent<Pickup>();
            Interact interaction = hit.collider.GetComponent<Interact>();

            if (IsPickup() && pickup != null) { pickup.test(); }
            else if (interaction != null) { interaction.Act(GetInputActionType()); }
        }
    }

    InputAction GetInputAction() {
        for (int i = 0; i < totalTypes; i++) {
            if (interactActions[i].WasPressedThisFrame()) return interactActions[i];
        }
        return null;
    }

    InteractionType GetInputActionType() {
        for (int i = 0; i < totalTypes; i++) {
            if (interactActions[i].WasPressedThisFrame()) return (InteractionType)i;
        }
        return InteractionType.Count;
    }

    // CHECKERS
    bool IsPickup() {
        return GetInputActionType() == InteractionType.LeftClick || GetInputActionType() == InteractionType.RightClick;
    }


    // SETTERS
    void SetTotalTypes() {
        totalTypes = (int)InteractionType.Count;
    }

    void SetInteractActions() {
        interactActions = new InputAction[totalTypes];

        for (int i = 0; i < totalTypes; i++) {
            interactActions[i] = InputManager.instance.GetAction(actionName, ((InteractionType)i).ToString());
        }
    }

}
