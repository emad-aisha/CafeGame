using UnityEngine;

// TODO: rename to more descriptive name
public class NeededType : MonoBehaviour {
    [SerializeField] protected InteractText interactText;
    [SerializeField] protected InteractionType neededInteractionType;
    [HideInInspector] public bool hasInteracted;

}
