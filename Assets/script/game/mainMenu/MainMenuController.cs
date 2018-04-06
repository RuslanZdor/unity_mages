using script;
using UnityEngine;

public class MainMenuController : GameScene, IListenerObject {

    void Start() {
        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;

        registerListener(this);
        isFinished = true;

        disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            openInventory();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            openSkills();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            openNewMission();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            openPositions();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            openShop();
        }
    }

    public void openShop() {
        navigation().closeActiveWindow();
        navigation().openShop();
    }

    public void openPositions() {
        navigation().closeActiveWindow();
        navigation().openPositions();
    }

    public void openSkills() {
        navigation().closeActiveWindow();
        navigation().openSkills();
    }

    public void openInventory() {
        navigation().closeActiveWindow();
        navigation().openInventory();
    }

    public void openNewMission() {
        navigation().closeActiveWindow();
        generateMessage(new GameMessage(MessageType.INIT_FIGHT_MAP));
        navigation().openFightMap();
    }

    public void readMessage(GameMessage message) {
       if (message.type == MessageType.OPEN_MAIN_MENU) {
            enable();
        }
        if (gameObject.activeInHierarchy && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
