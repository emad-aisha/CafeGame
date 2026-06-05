using UnityEngine;

public class Pour : Interactable {

    public override void Interact(string interactType) {
        if (!CanInteract(interactType)) return;


    }

}
