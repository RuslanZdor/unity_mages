using UnityEngine;
using UnityEngine.EventSystems;

public class PositionsController : GameScene, IListenerObject, CanReload {

    private GameObject activeHeroes;
    private GameObject waitingHeroes;

    public GameObject heroImage;

    void Start() {
        activeHeroes = transform.Find("activeHeroes").gameObject;
        waitingHeroes = transform.Find("waitingHeroes").gameObject;
        disable();
        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            close();
        }
    }

    public void reload() {
        activeHeroes.GetComponent<ActiveHeroesController>().reload();
        waitingHeroes.GetComponent<WaitingHeroesController>().reload();
    }

    public void close() {
        generateMessage(new GameMessage(MessageType.CLOSE_POSITIONS));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_POSITIONS) {
            enable();
            reload();
        }
        if (message.type == MessageType.CLOSE_POSITIONS) {
            disable();
        }
    }
}
