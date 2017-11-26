using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthObjectController : MonoBehaviour {

    private float lastIndicatorShow = 0.0f;
    private float durationIndicatorShow = 1.0f;

    private Person person;
    private Slider healthSlider;
    private GameObject damageIndicator;
    private GameObject healIndicator;
    private GameObject indicatorsBackground;

    void Start() {
        person = transform.GetComponent<PersonController>().person;

        healthSlider = transform.Find("healthBar/slider").GetComponent<Slider>();
        if (healthSlider != null && person != null) {
            healthSlider.maxValue = person.maxHealth;
            healthSlider.value = person.maxHealth;
        }

        damageIndicator = transform.Find("indicators/damageIndicator").gameObject;
        damageIndicator.GetComponent<Text>().enabled = false;
        healIndicator = transform.Find("indicators/healIndicator").gameObject;
        indicatorsBackground = transform.Find("indicators").gameObject;
        indicatorsBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    void FixedUpdate() {
        if (lastIndicatorShow + durationIndicatorShow < Time.fixedTime) {
            healIndicator.GetComponent<Text>().enabled = false;
            damageIndicator.GetComponent<Text>().enabled = false;
            indicatorsBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

        if (healthSlider != null && person != null) {
            if (healthSlider.value != person.health) {
                lastIndicatorShow = Time.fixedTime;
                if (healthSlider.value > person.health) {
                    indicatorsBackground.GetComponent<Image>().color = new Color(((float) 245 / 256), ((float) 48 / 256), ((float)48 / 256), ((float) 157 / 256));

                    healIndicator.GetComponent<Text>().enabled = false;
                    damageIndicator.GetComponent<Text>().enabled = true;
                    damageIndicator.GetComponent<Text>().text = (healthSlider.value - person.health).ToString();

                } else {
                    indicatorsBackground.GetComponent<Image>().color = new Color(((float) 55 / 256), ((float) 244 / 256), ((float) 48 / 256), ((float)157 / 256));

                    healIndicator.GetComponent<Text>().enabled = true;
                    damageIndicator.GetComponent<Text>().enabled = false;
                    healIndicator.GetComponent<Text>().text = (person.health - healthSlider.value).ToString();

                }

                healthSlider.value = person.health;
            }

        }
    }
}
