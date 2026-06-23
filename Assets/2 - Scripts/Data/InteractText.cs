using UnityEngine;

[CreateAssetMenu(fileName = "InteractText", menuName = "InteractText")]
public class InteractText : ScriptableObject {
    [SerializeField] private string text;
    [SerializeField] private float timeOnScreen;

    [HideInInspector] public string Text => text;
    [HideInInspector] public float TimeOnScreen => timeOnScreen;
}
