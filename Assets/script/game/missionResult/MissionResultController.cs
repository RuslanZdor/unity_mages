using UnityEngine;
using System.Collections.Generic;

public class MissionResultController : GameScene, IListenerObject {

    private GameObject result;
    private GameObject resultHeader;

    public GameObject personStatistics;

    public List<Party> parties = new List<Party>();

    void Start() {
        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;


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
        generateMessage(new GameMessage(MessageType.CLOSE_MISSION_RESULT));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    private void reload() {
        foreach (Person p in PartiesSingleton.activeHeroes) {
            p.setExpirience(p.experience + 100);
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_MISSION_RESULT) {
            enable();
            reload();
        }
        if (message.type == MessageType.CLOSE_MISSION_RESULT) {
            disable();
        }
    }
}
