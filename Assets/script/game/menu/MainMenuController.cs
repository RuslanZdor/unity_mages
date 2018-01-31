using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuController : GameScene, IListenerObject {

    void Start() {
        if (!isFinished) {
            background = "texture/main_scene";
            Sprite image = Resources.Load<Sprite>(background) as Sprite;
            transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;

            registerListener(this);
            isFinished = true;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            openInventory();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            openSkills();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            openFight();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            openPositions();
        }
    }

    public void openFight() {
        generateMessage(new GameMessage(MessageType.INIT_FIGHT_MAP));
        generateMessage(new GameMessage(MessageType.OPEN_FIGHT_MAP));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void openInventory() {
        generateMessage(new GameMessage(MessageType.OPEN_INVENTORY));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void openSkills() {
        generateMessage(new GameMessage(MessageType.OPEN_SKILLS));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void openPositions() {
        generateMessage(new GameMessage(MessageType.OPEN_POSITIONS));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
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
