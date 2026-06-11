using UnityEngine;
using UnityEngine.InputSystem;

public class Test : Interactable {
    override public void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;

        Debug.Log("Interact: " + interactType);
    }

}
