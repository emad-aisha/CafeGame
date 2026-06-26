using System.Collections;
using TMPro;
using UnityEngine;

using static MenuManagerHelper;

// TODO: change into seperate managers?
public class MenuManager : MonoBehaviour {
    [HideInInspector]
    public static MenuManager instance;
    private MenuManagerHelper Helper;

    // TODO: organize PLEASE
    [SerializeField] Transform canvasParent;

    [Header("Interact Popups")]
    [SerializeField] GameObject flashInteractObject;
    [SerializeField] GameObject setInteractObject;
    [SerializeField] int tranformIncrement;

    [Header("Holding Bar UI")]
    [SerializeField] GameObject holdBarObject;
    [SerializeField] GameObject greenRange;
    [SerializeField] GameObject holdRange;

    void Awake() {
        if (instance == null) instance = this;
        SetHelper();
    }

    void SetHelper() {
        Helper = new();
        Helper.SetHoldBarData(greenRange, holdRange);
        Helper.SetInteractData(setInteractObject);

    }


    void Update() {
        Helper.UpdateInteract();
    }

    // INTERACT POPUP ========================================================================
    // FLASH
    public IEnumerator FlashInteract(InteractText interactText, float time = 0f) {
        Helper.IncFlashObjects();
        Helper.IncObjects();
        GameObject flashObject = Instantiate(flashInteractObject, canvasParent);
        flashObject.SetActive(true);

        // set position + text
        flashObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, tranformIncrement * Helper.GetFlashObjects());
        flashObject.GetComponent<TMP_Text>().text = interactText.Text;

        if (time == 0) time = interactText.TimeOnScreen;
        yield return new WaitForSeconds(time);

        Destroy(flashObject);
        Helper.DecObjects();
    }

    // MANUAL
    public void EnableText(InteractText interactText) {
        Helper.SetText(interactText.Text);
        setInteractObject.SetActive(true);
    }

    public void DisableText() {
        setInteractObject.SetActive(false);
        Helper.SetText("");
    }


    // HOLD BAR ==============================================================================
    public void ShowHoldBar(float min, float width) {
        Helper.SetBarPosition(Type.Green, min);
        Helper.SetBarWidth(Type.Green, width);

        holdBarObject.SetActive(true);
    }

    public void HideHoldBar() {
        Helper.SetBarPosition(Type.Green, 0, 0);
        Helper.SetBarWidth(Type.Green, 0);
        Helper.SetBarWidth(Type.Hold, 0);

        holdBarObject.SetActive(false);
    }

    public void UpdateHoldBar(float holdValue) {
        Helper.SetBarWidth(Type.Hold, holdValue);
    }

}
