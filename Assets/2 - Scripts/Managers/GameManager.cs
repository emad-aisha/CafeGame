using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public CameraController cameraController;

    void Awake() {
        if (instance == null) instance = this;

        cameraController = Camera.main.GetComponent<CameraController>();
    }


}
