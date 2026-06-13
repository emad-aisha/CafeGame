using UnityEngine;

public class NeededType : MonoBehaviour {
    [SerializeField] protected InteractionType neededInteractionType;
    [HideInInspector] public bool hasInteracted;

}
