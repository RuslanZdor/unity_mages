using UnityEngine;
using UnityEngine.UI;

public class AbilityEventController : MonoBehaviour {

    public Event abilityEvent;

    public void setEvent(Event ev) {
        abilityEvent = ev;
        transform.Find("Ability").transform.GetComponent<Image>().sprite = abilityEvent.ability.image;
    }
}
