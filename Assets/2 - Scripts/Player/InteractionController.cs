using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InteractionType { LeftClick, RightClick, Primary, Secondary, [InspectorName(null)] MouseMovement, [InspectorName(null)] Count };
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
            Interact();
        }
        else {
            internalInteractTimer -= Time.deltaTime;
        }
    }

    void Interact() {
        RaycastHit hit;
        bool hitInteractable = Physics.Raycast(GameManager.instance.mainCamera.transform.position, GameManager.instance.mainCamera.transform.forward,
            out hit, distance, ~ignoreLayer);

        if (hitInteractable) {
            IInteractable interaction = hit.collider.GetComponent<IInteractable>();
            if (interaction != null) { interaction.Interact(GetInputActionType()); }
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


    // SETTERS
    void SetTotalTypes() {
        totalTypes = (int)InteractionType.Count - 1; // Don't include Mouse Movement
    }

    void SetInteractActions() {
        interactActions = new InputAction[totalTypes];

        for (int i = 0; i < totalTypes; i++) {
            interactActions[i] = InputManager.instance.GetAction(actionName, ((InteractionType)i).ToString());
        }
    }

}
