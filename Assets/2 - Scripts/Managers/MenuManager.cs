using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: change into seperate managers?
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

    // interact text =====================================
    // flash on screen
    // TODO: clean this up
    public IEnumerator FlashInteract(InteractText interactText, float time = 0f) {
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

        if (time == 0) time = interactText.TimeOnScreen;
        yield return new WaitForSeconds(time);

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



    [SerializeField] GameObject holdBarObject;
    [SerializeField] GameObject greenRange;
    [SerializeField] GameObject holdRange;
    // holding =====================================
    // TODO: toggle?
    public void ShowHoldBar(float min, float max) {
        // TODO: make this not affect not needed variables
        greenRange.GetComponent<RectTransform>().anchoredPosition = new Vector2(min, 0);
        greenRange.GetComponent<RectTransform>().sizeDelta = new Vector2(max, greenRange.GetComponent<RectTransform>().sizeDelta.y);
        holdRange.GetComponent<RectTransform>().sizeDelta = new Vector2(0, holdRange.GetComponent<RectTransform>().sizeDelta.y);

        holdBarObject.SetActive(true);
    }

    public void HideHoldBar() {
        greenRange.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        greenRange.GetComponent<RectTransform>().sizeDelta = new Vector2(0, greenRange.GetComponent<RectTransform>().sizeDelta.y);
        holdRange.GetComponent<RectTransform>().sizeDelta = new Vector2(0, holdRange.GetComponent<RectTransform>().sizeDelta.y);

        holdBarObject.SetActive(false);
    }

    public void UpdateHoldBar(float holdValue) {
        holdRange.GetComponent<RectTransform>().sizeDelta = new Vector2(holdValue, holdRange.GetComponent<RectTransform>().sizeDelta.y);
    }

}
