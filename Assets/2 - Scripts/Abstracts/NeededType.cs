using UnityEngine;

// TODO: rename to more descriptive name
public class NeededType : MonoBehaviour {
    [SerializeField] protected InteractText interactText;
    [SerializeField] protected InteractionType interactType;

    [HideInInspector] public bool hasInteracted;


    protected bool InteractTypeCheck(InteractionType newInteractType) {
        return newInteractType == interactType;
    }


}
