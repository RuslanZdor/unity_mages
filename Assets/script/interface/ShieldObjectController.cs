using UnityEngine;
using UnityEngine.UI;

public class ShieldObjectController : MonoBehaviour {

    public GameObject indicator;
    private GameObject indicators;

    private Person person;

    private Slider healthSlider;
    private Text count;

    void Start() {
        person = transform.GetComponent<PersonController>().person;
        indicators = transform.Find("indicators").gameObject;
        healthSlider = transform.Find("shieldBar/slider").GetComponent<Slider>();
        count = transform.Find("shieldBar/count").GetComponent<Text>();
        if (healthSlider != null && person != null) {
            healthSlider.maxValue = person.maxHealth;
            healthSlider.value = person.shield;
            count.text = person.shield.ToString();
        }
    }

    void FixedUpdate() {
        if (healthSlider != null && person != null) {
            if (healthSlider.value != person.shield) {
                if (healthSlider.value > person.shield) {
                    GameObject damageIndicator = Instantiate(indicator, indicators.transform, false);
                    damageIndicator.GetComponent<IndicatorController>().loseShield(healthSlider.value - person.shield);
                } else {
                    GameObject healIndicator = Instantiate(indicator, indicators.transform, false);
                    healIndicator.GetComponent<IndicatorController>().addShield(person.shield - healthSlider.value);
                }

                healthSlider.value = person.shield;
                count.text = person.shield.ToString();
            }

        }
    }
}
