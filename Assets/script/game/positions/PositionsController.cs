using UnityEngine;

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
        base.Update();

        if (isActive) {
            if (Input.GetKeyDown(KeyCode.P)) {
                close();
            }
        }
    }

    public void reload() {
        activeHeroes.GetComponent<ActiveHeroesController>().reload();
        waitingHeroes.GetComponent<WaitingHeroesController>().reload();
    }

    public void close() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_POSITIONS) {
            enable();
            reload();
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
