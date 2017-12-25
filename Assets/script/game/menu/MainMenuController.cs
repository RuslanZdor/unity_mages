using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuController : GameScene, IListenerObject {

    public GameScene fight;
    public GameScene inventory;

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
        if (Input.GetKeyDown(KeyCode.I) && !isHided) {
            openInventory();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isHided) {
            openFight();
        }
    }

    public void openFight() {
        GameMessage gm = new GameMessage();
        gm.type = MessageType.INIT_FIGHT_MAP;
        gm.message = "init fight map";
        generateMessage(gm);

        GameMessage gm2 = new GameMessage();
        gm2.type = MessageType.OPEN_FIGHT_MAP;
        gm2.message = "open fight map";
        generateMessage(gm2);

        GameMessage gm3 = new GameMessage();
        gm3.type = MessageType.CLOSE_MAIN_MENU;
        gm3.message = "open inventory";
        generateMessage(gm3);
    }

    public void openInventory() {
        GameMessage gm = new GameMessage();
        gm.type = MessageType.OPEN_INVENTORY;
        gm.message = "open inventory";
        generateMessage(gm);

        GameMessage gm2 = new GameMessage();
        gm2.type = MessageType.CLOSE_MAIN_MENU;
        gm2.message = "close main menu";
        generateMessage(gm2);
    }

    public void readMessage(GameMessage message) {
       if (message.type == MessageType.OPEN_MAIN_MENU) {
            gameObject.SetActive(true);
            isHided = false;
        }
        if (message.type == MessageType.CLOSE_MAIN_MENU) {
            gameObject.SetActive(false);
            isHided = true;
        }
    }
}
