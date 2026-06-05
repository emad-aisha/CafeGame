using UnityEngine;
using UnityEngine.InputSystem;

public class Blender : Interactable {
    bool hasItem;

    public override void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;
        hasItem = !hasItem;
        Debug.Log("Has Item: " + hasItem);
    }


    public bool HasItem() { return hasItem; }
}
