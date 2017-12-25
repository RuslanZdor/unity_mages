using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PersonController : MonoBehaviour {

    public Person person;

    public Animator animator;
    public GameObject heroLevel;

    void Start() {
        animator = transform.Find("model").GetComponent<Animator>();
        heroLevel = transform.Find("HeroLevel").gameObject;
        heroLevel.transform.GetComponent<Text>().text = person.level.ToString();
    }

    void Update() {
        if (person.health <= 0) {
            person.isAlive = false;
            setDead();
        }
    }


    public void castAbility() {
        if (!EventQueueSingleton.queue.fastFight) {
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISCAST);
        }
    }

    public void meleeAttackAbility() {
        if (!EventQueueSingleton.queue.fastFight) {
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISATTACK);
        }
    }

    public void hittenTrigger() {
        if (!EventQueueSingleton.queue.fastFight) {
            animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISHITTEN);
        }
    }

    public void setDead() {
        if (!EventQueueSingleton.queue.fastFight) {
            animator.SetBool(AnimatorConstants.MODEL_ANIMATOR_ISDEAD, true);
        }
    }

}
