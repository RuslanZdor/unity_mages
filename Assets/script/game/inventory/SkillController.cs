using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillController : PersonBehavior, IPointerClickHandler {

	public Ability ability;

    private bool isBlocked;

    public void OnPointerClick(PointerEventData eventData) {
        if (!isBlocked) {
            foreach (var ab in person.knownAbilities) {
                if (ab.requiredLevel == ability.requiredLevel) {
                    ab.isActive = false;
                }
            }
            ability.isActive = true;

            transform.parent.parent.GetComponent<HeroTabController>().reload();
        }
    }

    public void blocked() {
        isBlocked = true;
        transform.GetComponent<Image>().color = new Color((float)136 / 256, (float)136 / 256, (float)136 / 256, (float)136 / 256);
    }

    public void enable() {
        transform.GetComponent<Image>().color = new Color((float)255 / 256, (float)255 / 256, (float)255 / 256, (float)255 / 256);
    }

    public void disable() {
        transform.GetComponent<Image>().color = new Color((float)255 / 256, (float)255 / 256, (float)255 / 256, (float)136 / 256);
    }
}
