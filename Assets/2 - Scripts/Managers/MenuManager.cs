using System.Collections;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    enum Type { Set, Flash };
    public static MenuManager instance;

    // TODO: make flash ones into array + move up 
    [SerializeField] GameObject setInteractObject;
    [SerializeField] TMP_Text setInteractionText;

    [SerializeField] GameObject flashInteractObject;
    [SerializeField] TMP_Text flashInteractionText;
    int ignore;

    void Awake() {
        if (instance == null) instance = this;
    }

    // flash on screen
    public IEnumerator FlashInteract(InteractText interactText) {
        SetText(interactText.Text, Type.Flash);
        flashInteractObject.SetActive(true);

        yield return new WaitForSeconds(interactText.TimeOnScreen);

        flashInteractObject.SetActive(false);
        ResetText(Type.Flash);
    }

    // manually turn on and off
    public void EnableText(InteractText interactText) {
        SetText(interactText.Text, Type.Set);
        setInteractObject.SetActive(true);
    }

    public void DisableText(InteractText interactText) {
        setInteractObject.SetActive(false);
        ResetText(Type.Set);
    }


    void SetText(string interactText, Type type) {
        switch (type) {
            case Type.Flash: flashInteractionText.text = interactText; break;
            case Type.Set: setInteractionText.text = interactText; break;
        }
    }

    void ResetText(Type type) {
        switch (type) {
            case Type.Flash: flashInteractionText.text = ""; break;
            case Type.Set: setInteractionText.text = ""; break;
        }
    }


}
