using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager instance;

    [SerializeField] GameObject moveMouseScreen;
    [SerializeField] GameObject holdMouseScreen;
    int ignore;

    void Awake() {
        if (instance == null) instance = this;
    }


    void ShowMenu(GameObject screen) {
        screen.SetActive(true);
    }

    void HideMenu(GameObject screen) {
        screen.SetActive(false);
    }
}
