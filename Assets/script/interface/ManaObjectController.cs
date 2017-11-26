using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaObjectController : MonoBehaviour {

    private Person person;
    private Slider slider;

    void Start() {
        person = transform.GetComponent<PersonController>().person;

        slider = transform.Find("manaBar/slider").GetComponent<Slider>();
        if (slider != null && person != null) {
            slider.maxValue = person.maxMana;
            slider.value = person.maxMana;
        }
    }

    void FixedUpdate() {
        if (slider != null && person != null) {
            if (slider.value != person.mana) {
                slider.value = person.mana;
            }
        }
    }
}
