using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EnemyIconController : MonoBehaviour, IPointerClickHandler {

    private MainMenuController mmController;

    // Use this for initialization
    void Start() {
        mmController = transform.parent.GetComponent<MainMenuController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) {
        mmController.openFight();
    }
}
