using UnityEngine;

// TODO: add a static camera
public class FaceCamera : MonoBehaviour {
    Camera dynamicCam;
    //Camera staticCam;

    void Start() {
        dynamicCam = GameManager.instance.mainCamera;
        //staticCam = GameManager.instance.staticCamera;
    }

    void Update() {
        if (dynamicCam == null) {
            //transform.LookAt(mainCam.transform.position);
        }
        else {
            transform.LookAt(dynamicCam.transform.position);
        }
    }
}
