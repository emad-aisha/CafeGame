using UnityEngine.UI;
using UnityEngine;

public class TimerBasedUI : MonoBehaviour {
    [SerializeField] GameObject bar;
    [SerializeField] Image holdRange;
    [SerializeField] Image greenRange;
    RectTransform greenTransform;
    float barWidth;

    Interact interactObject;

    void Start() {
        interactObject = transform.parent.gameObject.GetComponent<Interact>();

        barWidth = bar.GetComponent<RectTransform>().rect.width;
        greenTransform = greenRange.GetComponent<RectTransform>();

        bar.SetActive(false);
    }

    void Update() {
        if (!interactObject.GetHasInteracted() && interactObject.GetInternalValue() == 0) End();
        else if (interactObject.GetHasInteracted() || interactObject.GetInternalValue() != 0) Begin();

        SetHoldValue(interactObject.GetInternalValue());
    }

    void Begin() {
        if (bar.activeSelf) return;
        bar.SetActive(true);
        SetGreenRange(interactObject.GetMinValue(), interactObject.GetMaxValue());
    }

    void End() {
        if (!bar.activeSelf) return;
        bar.SetActive(false);
        SetHoldValue(0);
        SetGreenRange(0, 0);
    }

    // setters
    void SetHoldValue(float value) {
        holdRange.fillAmount = value;
    }

    void SetGreenRange(float min, float max) {
        greenTransform.anchoredPosition = new Vector2(min * barWidth, greenTransform.anchoredPosition.y);
        greenTransform.sizeDelta = new Vector2((max * barWidth) - (min * barWidth), greenTransform.sizeDelta.y);
    }

}
