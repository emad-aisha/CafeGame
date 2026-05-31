using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] string actionName;
    CharacterController controller;
    InputAction moveAction;
    InputAction sprintAction;
    InputAction jumpAction;

    [SerializeField] int speed;
    [SerializeField, Range(1, 5)] float sprintMod;
    [SerializeField] float jumpSpeed;

    [SerializeField] float gravity;

    Vector3 moveDirection;
    Vector3 moveInput;
    Vector3 jumpVelocity;

    bool isSprinting = false;
    bool isJumping = false;

    void OnEnable() {
        inputActions.FindActionMap(actionName).Enable();
    }

    void OnDisable() {
        inputActions.FindActionMap(actionName).Disable();
    }


    void Start() {
        controller = GetComponent<CharacterController>();

        // set actions
        moveAction = inputActions.FindActionMap(actionName).FindAction("Move");
        sprintAction = inputActions.FindActionMap(actionName).FindAction("Sprint");
        jumpAction = inputActions.FindActionMap(actionName).FindAction("Jump");


    }

    void Update() {
        MovementSystem();
    }

    void MovementSystem() {
        // sprint logic
        isSprinting = sprintAction.IsPressed();

        // moving logic
        moveInput = moveAction.ReadValue<Vector2>();
        moveDirection = moveInput.x * transform.right + moveInput.y * transform.forward + jumpVelocity.y * transform.up;
        if (isSprinting) {
            moveDirection.x *= sprintMod;
            moveDirection.y *= sprintMod;
        }
        controller.Move(moveDirection * speed * Time.deltaTime);


        // TODO: may not need
        // jump logic
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
