using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PersonController : MonoBehaviour {

    public Person person;

    public Animator animator;

    void Start() {
        animator = transform.Find("model").GetComponent<Animator>();
    }

    void Update() {
        if (person.health <= 0) {
            person.isAlive = false;
            animator.SetBool(AnimatorConstants.MODEL_ANIMATOR_ISDEAD, true);
        }
    }
}
