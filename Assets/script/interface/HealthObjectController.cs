using UnityEngine;
using UnityEngine.UI;

public class HealthObjectController : MonoBehaviour {

    public GameObject indicator;
    private GameObject indicators;

    private Person person;

    private Slider healthSlider;
    private Text count;

    void Start() {
        person = transform.GetComponent<PersonController>().person;
        indicators = transform.Find("indicators").gameObject;
        healthSlider = transform.Find("healthBar/slider").GetComponent<Slider>();
        count = transform.Find("healthBar/count").GetComponent<Text>();
        if (healthSlider != null && person != null) {
            healthSlider.maxValue = person.maxHealth;
            healthSlider.value = person.maxHealth;
            count.text = person.maxHealth.ToString();
        }
    }

    void FixedUpdate() {
        if (healthSlider != null && person != null) {
            if (healthSlider.value != person.health) {
                if (healthSlider.value > person.health) {
                    GameObject damageIndicator = Instantiate(indicator, indicators.transform, false);
                    damageIndicator.GetComponent<IndicatorController>().damage(healthSlider.value - person.health);
                } else {
                    GameObject healIndicator = Instantiate(indicator, indicators.transform, false);
                    healIndicator.GetComponent<IndicatorController>().heal(person.health - healthSlider.value);
                }

                healthSlider.value = person.health;
                count.text = person.health.ToString();
            }

        }
    }
}
