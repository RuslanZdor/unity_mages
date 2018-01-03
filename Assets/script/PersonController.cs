using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PersonController : MonoBehaviour {

    public Person person;

    public Animator animator;
    public Animator applyAbility;

    public GameObject heroLevel;

    void Start() {
        animator = transform.Find("model").GetComponent<Animator>();
        applyAbility = transform.Find("model/animation/abilityAnimation").GetComponent<Animator>();

        heroLevel = transform.Find("HeroLevel").gameObject;
        heroLevel.transform.GetComponent<Text>().text = person.level.ToString();

        if (person.itemList.FindAll((Item i) => i.type == ItemType.WEAPON).Count > 0) {
            Item item = person.itemList.FindAll((Item i) => i.type == ItemType.WEAPON)[0];
            transform.Find("model/person/body/leftHand/topHand/middleHand/bottomHand/weapon")
                .GetComponent<SpriteRenderer>().sprite = item.image;
        }
        if (person.itemList.FindAll((Item i) => i.type == ItemType.SHIELD).Count > 0) {
            Item item = person.itemList.FindAll((Item i) => i.type == ItemType.SHIELD)[0];
            transform.Find("model/person/body/rightHand/topHand/middleHand/bottomHand/shield")
                .GetComponent<SpriteRenderer>().sprite = item.image;
        }
    }

    void Update() {
        if (person.health <= 0) {
            person.isAlive = false;
            setDead();
        }
    }

    public void applyEffect() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting spell");
            applyAbility.SetTrigger(AnimatorConstants.MODEL_APPLY_ANIMATION);
        }
    }

    public void castAbility() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting spell");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISCAST);
        }
    }

    public void meleeAttackAbility() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " start casting attack");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISATTACK);
        }
    }

    public void hittenTrigger() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " been hitten");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISHITTEN);
        }
    }

    public void blockTrigger() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " been hitten, person name blocked attack");
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISBLOCK);
        }
    }

    public void setDead() {
        if (!EventQueueSingleton.queue.fastFight) {
            Debug.Log(Time.fixedTime + ":" + person.name + " is dead");
            animator.SetBool(AnimatorConstants.MODEL_ANIMATOR_ISDEAD, true);
        }
    }

}
