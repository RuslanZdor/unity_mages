using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserAbilityController : MonoBehaviour, IPointerClickHandler {

    public Ability ability;

    private Text count;
    private bool isDisable;

    public void Start() {
        count = transform.Find("Count/Text").GetComponent<Text>();
    }

    public void Update() {
        if (!isDisable) {
            count.text = ability.playerCastCount.ToString();
            if (ability.playerCastCount <= 0) {
                disable();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!isDisable) {
            PartiesSingleton.player.eventStart(ability, 0.0f);
        }
    }

    public void disable() {
        transform.Find("Count").gameObject.SetActive(false);
        transform.GetComponent<Image>().color = new Color((float)50 / 255, (float)50 / 255, (float)50 / 255, 1);
        isDisable = true;
    }
}
