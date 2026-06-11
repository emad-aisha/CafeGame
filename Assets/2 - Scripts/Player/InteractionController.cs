using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InteractionType { LeftClick, RightClick, Primary, Secondary, [InspectorName(null)] Count };
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
            Interact(GetInputAction());
        }
        else {
            internalInteractTimer -= Time.deltaTime;
        }
    }

    void Interact(InputAction interactType) {
        RaycastHit hit;
        bool hitInteractable = Physics.Raycast(GameManager.instance.mainCamera.transform.position, GameManager.instance.mainCamera.transform.forward,
            out hit, distance, ~ignoreLayer);

        if (hitInteractable) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable.CanInteract(interactType)) { interactable.Interact(interactType); }
        }
    }

    InputAction GetInputAction() {
        for (int i = 0; i < totalTypes; i++) {
            if (interactActions[i].WasPressedThisFrame()) return interactActions[i];
        }
        return null;
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
