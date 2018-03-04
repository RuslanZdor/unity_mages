using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuController : GameScene, IListenerObject {

    void Start() {
        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;

        registerListener(this);
        isFinished = true;

        disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            navigation().openInventory();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            navigation().openSkills();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            navigation().openFight();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            navigation().openPositions();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            navigation().openShop();
        }
    }

    public void readMessage(GameMessage message) {
       if (message.type == MessageType.OPEN_MAIN_MENU) {
            enable();
        }
        if (message.type == MessageType.CLOSE_MAIN_MENU) {
            disable();
        }
    }
}
