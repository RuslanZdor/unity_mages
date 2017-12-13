using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FightSkipController : MonoBehaviour, IPointerClickHandler {

    private FightStartController mmController;

    // Use this for initialization
    void Start() {
        mmController = transform.parent.GetComponent<FightStartController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) {
        mmController.skipFight();
    }
}
