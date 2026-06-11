using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public CameraController cameraController;
    public Camera mainCamera;

    void Awake() {
        if (instance == null) instance = this;

        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
    }


}
