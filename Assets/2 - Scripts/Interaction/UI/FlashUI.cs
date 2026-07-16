using UnityEngine;
using System.Collections;
using TMPro;

public class FlashUI : MonoBehaviour {
    [SerializeField] RectTransform canvasParent;

    [Header("")]
    [SerializeField] GameObject baseObject;
    [SerializeField] float tranformIncrement = 30;

    Interact interactObject;
    static int totalObjects;
    int activeObjects;


    void Start() {
        interactObject = transform.parent.gameObject.GetComponent<Interact>();
    }

    void Update() {
        if (totalObjects == 0 && activeObjects != 0) activeObjects--;

        if (interactObject.GetHasInteracted() || interactObject.GetInternalValue() != 0) {
            StartCoroutine(FlashInteract(interactObject.GetInteractPopup(), interactObject.GetPopupTime()));
        }
    }


    public IEnumerator FlashInteract(string interactText, float time) {
        activeObjects++;
        totalObjects++;
        GameObject flashObject = Instantiate(baseObject, canvasParent);
        flashObject.SetActive(true);

        // set position + text
        flashObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, tranformIncrement * totalObjects);
        flashObject.GetComponent<TMP_Text>().text = interactText;

        yield return new WaitForSeconds(time);

        Destroy(flashObject);
        totalObjects--;
    }


}
