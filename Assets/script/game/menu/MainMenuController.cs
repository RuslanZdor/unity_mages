using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuController : GameScene {

    public GameScene fight;
    public GameScene inventory;

    void Start() {
        if (!isFinished) {
            background = "texture/main_scene";
            Sprite image = Resources.Load<Sprite>(background) as Sprite;
            transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I) && !isFinished) {
            openInventory();
        }
        if (Input.GetKeyDown(KeyCode.F) && !isFinished) {
            openFight();
        }
    }

    public void openInventory() {
        setNextScene(inventory);
        inventory.setPreviousScene(this);
        isHided = true;
    }
    public void openFight() {
        setNextScene(fight);
        inventory.setPreviousScene(this);
        isHided = true;
    }
}
