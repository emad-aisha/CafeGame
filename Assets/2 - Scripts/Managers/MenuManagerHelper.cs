using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerHelper {
    // INTERACT =========================
    TMP_Text manualInteractText;
    int flashObjects = -1;
    public int objects = 0;

    // HOLD =============================
    public enum Type { Green, Hold };
    Image greenImage;
    Image holdImage;

    RectTransform greenTransform;
    RectTransform holdTransform;


    // SETTERS ===============================================================================
    public MenuManagerHelper() { }

    public void SetInteractData(GameObject setInteractObject) {
        manualInteractText = setInteractObject.GetComponent<TMP_Text>();
    }

    public void SetHoldBarData(GameObject greenRange, GameObject holdRange) {
        greenTransform = greenRange.GetComponent<RectTransform>();
        holdTransform = holdRange.GetComponent<RectTransform>();

        greenImage = greenRange.GetComponent<Image>();
        holdImage = holdRange.GetComponent<Image>();
    }


    // INTERACT POPUP ========================================================================
    public void UpdateInteract() { if (objects == 0) DecFlashObjects(); }

    public void SetText(string value = "") { manualInteractText.text = value; }

    public float GetFlashObjects() { return flashObjects; }
    public void IncFlashObjects() { flashObjects++; }
    public void DecFlashObjects() { if (flashObjects > -1) flashObjects--; }

    public void IncObjects() { objects++; }
    public void DecObjects() { objects--; }


    // HOLD BAR ==============================================================================
    public void SetBarPosition(Type type, float x) {
        switch (type) {
            case Type.Green: greenTransform.anchoredPosition = new Vector2(x, 0); break;
            case Type.Hold: holdTransform.anchoredPosition = new Vector2(x, 0); break;
        }
    }

    public void SetBarWidth(Type type, float value) {
        switch (type) {
            case Type.Green: greenImage.fillAmount = value; break;
            case Type.Hold: holdImage.fillAmount = value; break;
        }
    }


}
