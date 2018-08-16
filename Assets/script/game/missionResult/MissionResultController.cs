using System.Collections.Generic;
using script;
using UnityEngine;

public class MissionResultController : GameScene, IListenerObject {

    public List<Party> parties = new List<Party>();

    void Start() {
        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;
        registerListener(this);
        disable();
    }

    void Update() {
        base.Update();
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                closeResults();
            }
        }
    }

    private void closeResults() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    private void reload() {
        foreach (var p in PartiesSingleton.currentGame.activeHeroes) {
            p.setExpirience(p.experience + 100);
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_MISSION_RESULT) {
            enable();
            reload();
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
