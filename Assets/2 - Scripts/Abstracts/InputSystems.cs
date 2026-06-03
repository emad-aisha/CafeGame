using UnityEngine;

public abstract class InputSystems : MonoBehaviour
{
    [SerializeField] protected string actionName;

    void OnEnable() { InputManager.instance.EnableAction(actionName); }
    void OnDisable() { InputManager.instance.DisableAction(actionName); }
}
