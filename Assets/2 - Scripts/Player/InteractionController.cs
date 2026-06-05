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
        for (int i = 0; i < totalTypes; i++) {
            if (interactActions[i].WasPressedThisFrame()) return interactActions[i].name;
        }
        return "";
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
