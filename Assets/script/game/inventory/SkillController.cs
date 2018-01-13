using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillController : MonoBehaviour, IPointerClickHandler {

	public Ability ability;
    public Person person;

    private bool isBlocked;
    private bool isActive;

    public void OnPointerClick(PointerEventData eventData) {
        if (!isBlocked) {
            foreach (Ability ab in person.knownAbilities) {
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
        transform.GetComponent<Image>().color = new Color(((float)136 / 256), ((float)136 / 256), ((float)136 / 256), ((float)136 / 256));
    }

    public void enable() {
        isActive = true;
        transform.GetComponent<Image>().color = new Color(((float)255 / 256), ((float)255 / 256), ((float)255 / 256), ((float)255 / 256));
    }

    public void disable() {
        isActive = false;
        transform.GetComponent<Image>().color = new Color(((float)255 / 256), ((float)255 / 256), ((float)255 / 256), ((float)136 / 256));
    }
}
