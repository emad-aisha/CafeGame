using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [SerializeField] string actionName;
    InputAction lookAction;

    [SerializeField] int lockMin;
    [SerializeField] int lockMax;
    [SerializeField, Range(1, 50)] int sensitivity;

    [SerializeField] bool showRaycast;

    Vector3 lookDirection;
    float upRotation = 0;

    void Start() {
        lookAction = InputManager.instance.GetAction(actionName, "Look");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        lookDirection = lookAction.ReadValue<Vector2>();
        lookDirection *= sensitivity * Time.deltaTime;

        upRotation -= lookDirection.y;

        // clamp to range
        upRotation = Math.Clamp(upRotation, lockMin, lockMax);

        transform.localRotation = Quaternion.Euler(upRotation, 0, 0);
        transform.parent.Rotate(lookDirection.x * Vector3.up);

        if (showRaycast) Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
    }
}
