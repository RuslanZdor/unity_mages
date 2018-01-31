using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityEventController : MonoBehaviour {

    public Event abilityEvent;

    public void setEvent(Event ev) {
        abilityEvent = ev;
        transform.Find("Ability").transform.GetComponent<Image>().sprite = abilityEvent.ability.image;
//        owner.transform.GetComponent<Image>().sprite = abilityEvent.owner.image;
    }
}
