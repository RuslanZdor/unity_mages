using script;
using UnityEngine;

public class MainMenuController : GameScene, IListenerObject {
    
    void Update() {
        base.Update();
    }

    void Start() {
        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;

        registerListener(this);
        isFinished = true;

        disable();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_MAIN_MENU) {
            Debug.Log("opne main menu");
            enable();
        }
        if (isActive && message.type == MessageType.OPEN_INVENTORY) {
            navigation().closeActiveWindow();
            navigation().openInventory();
        }
        if (isActive && message.type == MessageType.OPEN_SHOP) {
            navigation().closeActiveWindow();
            navigation().openShop();
        }
        if (isActive && message.type == MessageType.OPEN_POSITIONS) {
            navigation().closeActiveWindow();
            navigation().openPositions();
        }
        if (isActive && message.type == MessageType.OPEN_SKILLS) {
            navigation().closeActiveWindow();
            navigation().openSkills();
        }
        if (isActive && message.type == MessageType.CLOSE_FIGHT_MAP) {
            navigation().closeActiveWindow();
            generateMessage(new GameMessage(MessageType.INIT_FIGHT_MAP));
            navigation().openFightMap();
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
