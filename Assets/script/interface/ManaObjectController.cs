using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaObjectController : MonoBehaviour {

    private Person person;

    private Slider slider;
    private Text count;

    void Start() {
        person = transform.GetComponent<PersonController>().person;

        slider = transform.Find("manaBar/slider").GetComponent<Slider>();
        count = transform.Find("manaBar/count").GetComponent<Text>();
        if (slider != null && person != null) {
            slider.maxValue = person.maxMana;
            slider.value = person.maxMana;
            count.text = person.maxMana.ToString();
        }
    }

    void FixedUpdate() {
        if (slider != null && person != null) {
            if (slider.value != person.mana) {
                slider.value = person.mana;
            }
        }

        count.text = person.mana.ToString();
    }
}
