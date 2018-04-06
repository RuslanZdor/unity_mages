using System.Collections.Generic;
using script;
using UnityEngine;

public class MissionResultController : GameScene, IListenerObject {

    private GameObject result;
    private GameObject resultHeader;

    public GameObject personStatistics;

    public List<Party> parties = new List<Party>();

    void Start() {
        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;


        result = transform.Find("ResultTable/table").gameObject;
        resultHeader = transform.Find("ResultTable/Header").gameObject;

        registerListener(this);
        disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            closeResults();
        }
    }

    private void closeResults() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    private void reload() {
        foreach (var p in PartiesSingleton.activeHeroes) {
            p.setExpirience(p.experience + 100);
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_MISSION_RESULT) {
            enable();
            reload();
        }
        if (gameObject.activeInHierarchy && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
