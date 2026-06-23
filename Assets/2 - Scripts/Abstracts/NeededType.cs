using UnityEngine;

public class NeededType : MonoBehaviour {
    [SerializeField] protected InteractText interactText;
    [SerializeField] protected InteractionType neededInteractionType;
    [HideInInspector] public bool hasInteracted;

}
