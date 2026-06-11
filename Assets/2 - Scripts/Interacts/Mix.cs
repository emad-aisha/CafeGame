using UnityEngine;
using UnityEngine.InputSystem;

public class Mix : Interactable {
    [SerializeField] float MixTimer;
    float internalTimer = 0;

    InputAction mixAction;
    Vector2 deltaMouse;

    bool isMixing = false;

    void Update() {
        if (!isMixing) return;

        deltaMouse = mixAction.ReadValue<Vector2>();
        if (deltaMouse.x == 0 && deltaMouse.y == 0) return;

        if (internalTimer >= MixTimer) {
            internalTimer = 0;
            isMixing = false;
            Debug.Log("Mixed");
            GameManager.instance.cameraController.Enable();

        }
        else {
            internalTimer += Time.deltaTime;
        }

    }

    override public void Interact(InputAction interactType) {
        if (!CanInteract(interactType)) return;
        isMixing = true;
        mixAction = InputManager.instance.GetAction("Interaction", "Mouse Movement");

        GameManager.instance.cameraController.Disable();
    }


}
