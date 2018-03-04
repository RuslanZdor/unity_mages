using UnityEngine;
using System.Collections;

public class NavigationController : MonoBehaviour {

    MessageController mc;

    public void generateMessage(GameMessage gm) {
        mc.addMessage(gm);
    }

    // Use this for initialization
    void Start() {
        mc = GameObject.Find("MessageController").GetComponent<MessageController>();
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

    public void closeInventory() {
        generateMessage(new GameMessage(MessageType.CLOSE_INVENTORY));
    }

    public void openSkills() {
        generateMessage(new GameMessage(MessageType.OPEN_SKILLS));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void openPositions() {
        generateMessage(new GameMessage(MessageType.OPEN_POSITIONS));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void openShop() {
        generateMessage(new GameMessage(MessageType.OPEN_SHOP));
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void closeShop() {
        generateMessage(new GameMessage(MessageType.CLOSE_SHOP));
    }

    public void openMainMenu() {
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void closeMainMenu() {
        generateMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));
    }

    public void saveGame() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
    }

    public void generateShop() {
        generateMessage(new GameMessage(MessageType.GENERATE_SHOP));
    }
}
