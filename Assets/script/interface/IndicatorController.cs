using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour {

    public float speed = 0.01f;
    public float liveTime = 3;

    public Color damageColor;
    public Color healColor;
    public Color plusShield;
    public Color minusShield;
    public Color manaColor;

    private float startedTime;

    // Use this for initialization
    void Start() {
        startedTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update() {
        if (startedTime + liveTime > Time.fixedTime) {
            Vector2 current = transform.position;
            float xSpeed = speed;
            if (transform.parent.parent.GetComponent<PersonController>().person.ally == AbilityTargetType.FRIEND) {
                xSpeed *= -1;
            }
            transform.position = new Vector2(current.x + xSpeed, current.y + speed);
        }else {
            Destroy(gameObject);
        }
    }

    public void damage(float value) {
        transform.GetComponent<Text>().color = damageColor;
        transform.GetComponent<Text>().text = value.ToString();
    }

    public void heal(float value) {
        transform.GetComponent<Text>().color = healColor;
        transform.GetComponent<Text>().text = value.ToString();
    }

    public void mana(float value) {
        transform.GetComponent<Text>().color = manaColor;
        transform.GetComponent<Text>().text = value.ToString();
    }

    public void loseShield(float value) {
        transform.GetComponent<Text>().color = minusShield;
        transform.GetComponent<Text>().text = value.ToString();
    }

    public void addShield(float value) {
        transform.GetComponent<Text>().color = plusShield;
        transform.GetComponent<Text>().text = value.ToString();
    }
}
