using UnityEngine;

public class Blender : Interactable {
    bool hasItem;

    public override void Interact(string interactType) {
        if (!CanInteract(interactType)) return;
        hasItem = !hasItem;
        Debug.Log("Has Item: " + hasItem);
    }


    public bool HasItem() { return hasItem; }
}
