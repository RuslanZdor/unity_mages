using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillsTabController : MonoBehaviour, IPointerClickHandler {

    public Person person;

    public void OnPointerClick(PointerEventData eventData) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).transform.GetComponent<Image>().color = new Color(((float)238 / 256), ((float)186 / 256), ((float)186 / 256), ((float)256 / 256));
        }

        for (int s = 0; s < person.abilityList.Count; s++) {
            person.abilityList[s].abilityTactic.defaultPriority = 1;
        }
    }
}
