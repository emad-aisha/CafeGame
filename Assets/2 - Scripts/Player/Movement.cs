using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [SerializeField] string actionName;
    CharacterController controller;
    InputAction moveAction;
    InputAction sprintAction;
    InputAction jumpAction;

    [SerializeField] int speed;
    [SerializeField, Range(1, 2)] float sprintMod;
    [SerializeField] float jumpSpeed;

    [SerializeField] float gravity;

    Vector3 moveDirection;
    Vector3 moveInput;
    Vector3 jumpVelocity;

    bool isSprinting = false;
    bool isJumping = false;

    void Start() {
        controller = GetComponent<CharacterController>();

        moveAction = InputManager.instance.GetAction(actionName, "Move");
        sprintAction = InputManager.instance.GetAction(actionName, "Sprint");
        jumpAction = InputManager.instance.GetAction(actionName, "Jump");
    }

    void Update() {
        MoveLogic();

        // TODO: may not need
        JumpLogic();
    }


    void MoveLogic() {
        isSprinting = sprintAction.IsPressed();

        // moving logic
        moveInput = moveAction.ReadValue<Vector2>();
        moveDirection = moveInput.x * transform.right + moveInput.y * transform.forward + jumpVelocity.y * transform.up;
        if (isSprinting) {
            Debug.Log("sprinting");
            moveDirection.x *= sprintMod;
            moveDirection.z *= sprintMod;
        }
        controller.Move(moveDirection * speed * Time.deltaTime);

    }

    void JumpLogic() {
        if (jumpAction.IsPressed() && !isJumping) {
            jumpVelocity.y = jumpSpeed;
            isJumping = true;
        }

        // gravity logic
        if (controller.isGrounded) {
            isJumping = false;
            jumpVelocity = Vector3.zero;
        }
        else {
            jumpVelocity.y -= gravity * Time.deltaTime;
        }
    }


}
