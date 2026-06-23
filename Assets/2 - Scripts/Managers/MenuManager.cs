using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    enum Type { Set, Flash };
    public static MenuManager instance;

    [SerializeField] Transform canvasParent;

    // TODO: make flash ones into array + move up 
    [SerializeField] GameObject setInteractObject;
    [SerializeField] TMP_Text setInteractionText;

    [SerializeField] GameObject flashInteractObject;

    [SerializeField] int tranformIncrement;
    int flashObjects;
    int objects;


    void Awake() {
        if (instance == null) instance = this;
        flashObjects = -1;
        objects = 0;
    }

    void Update() {
        if (objects == 0) { DecrementFlashObjects(); }
    }

    // flash on screen
    public IEnumerator FlashInteract(InteractText interactText) {
        flashObjects++;
        GameObject flashObject = Instantiate(flashInteractObject);
        flashObject.transform.SetParent(canvasParent);
        flashObject.SetActive(true);
        objects++;

        Vector2 textTransform = Vector2.zero;
        textTransform.y = tranformIncrement * flashObjects;

        flashObject.GetComponent<RectTransform>().anchoredPosition += textTransform;

        TMP_Text testRefernce = flashObject.GetComponent<TMP_Text>();
        testRefernce.text = interactText.Text;

        yield return new WaitForSeconds(interactText.TimeOnScreen);

        testRefernce.text = "";
        flashObject.SetActive(false);
        Destroy(flashObject);
        objects--;
    }

    // manually turn on and off
    public void EnableText(InteractText interactText) {
        SetText(interactText.Text);
        setInteractObject.SetActive(true);
    }

    public void DisableText() {
        setInteractObject.SetActive(false);
        ResetText();
    }

    public void DecrementFlashObjects() {
        if (flashObjects > -1) flashObjects--;
    }

    void SetText(string interactText) {
        setInteractionText.text = interactText;
    }

    void ResetText() {
        setInteractionText.text = "";
    }


}
