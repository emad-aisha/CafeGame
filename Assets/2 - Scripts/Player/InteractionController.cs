using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : InputSystems {
    [SerializeField] int distance;
    [SerializeField] float interactTimer;
    float internalInteractTimer;

    [SerializeField] LayerMask ignoreLayer;

    InputAction leftClickAction;
    InputAction rightClickAction;
    InputAction primaryAction;
    InputAction secondaryAction;

    void Start() {
        leftClickAction = InputManager.instance.GetAction(actionName, "Left Click");
        rightClickAction = InputManager.instance.GetAction(actionName, "Right Click");
        primaryAction = InputManager.instance.GetAction(actionName, "Primary Interact");
        secondaryAction = InputManager.instance.GetAction(actionName, "Secondary Interact");
    }

    void Update() {
        //ClickInteract();
        //ButtonInteract();
        InteractCheck();
    }


    void ClickInteract() {
        if (leftClickAction.WasPressedThisFrame() || rightClickAction.WasPressedThisFrame()) Debug.Log("click");
    }

    void ButtonInteract() {
        if (primaryAction.WasPressedThisFrame() || secondaryAction.WasPressedThisFrame()) Debug.Log("button");
    }

    void InteractCheck() {
        if (internalInteractTimer <= 0) {
            if (TryInteract() == "") return;
            internalInteractTimer = interactTimer;
            Interact(TryInteract());
        }
        else {
            internalInteractTimer -= Time.deltaTime;
        }
    }

    void Interact(string interactType) {
        RaycastHit hit;
        bool hitInteractable = Physics.Raycast(GameManager.instance.cameraController.transform.position, GameManager.instance.cameraController.transform.forward,
            out hit, distance, ~ignoreLayer);

        if (hitInteractable) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null) interactable.Interact(interactType);
        }
    }


    string TryInteract() {
        if (leftClickAction.WasPressedThisFrame()) return leftClickAction.name;
        if (rightClickAction.WasPressedThisFrame()) return rightClickAction.name;
        if (primaryAction.WasPressedThisFrame()) return primaryAction.name;
        if (secondaryAction.WasPressedThisFrame()) return secondaryAction.name;
        return "";
    }

}
