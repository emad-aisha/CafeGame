using UnityEngine;

public class Test : Interactable {
    override public void Interact(string interactType) {
        if (!CanInteract(interactType)) return;

        Debug.Log("Testing Testing, One Two Three... " + interactType);
    }

    protected override bool CanInteract(string interactType) {
        if (!MatchType(interactType)) return false;
        return true;
    }
}
