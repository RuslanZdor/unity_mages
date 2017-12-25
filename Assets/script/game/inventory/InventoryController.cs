using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : GameScene, IListenerObject {

    private GameObject heroTab;
    private GameObject heroList;
    private GameObject heroItems;

    public GameObject heroImage;

    private GameObject dragObject;

    void Start() {
        heroTab = transform.Find("HeroTab").gameObject;
        heroList = transform.Find("HeroList").gameObject;
        heroItems = transform.Find("Inventory").gameObject;

        reload();

        gameObject.SetActive(false);

        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I) && !isFinished) {
            closeInventory();
        }
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().person = PartiesSingleton.activeHeroes[0];
        heroList.GetComponent<HeroListController>().reload();

        heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().reload();

        heroItems.GetComponent<HeroItemsController>().person = PartiesSingleton.activeHeroes[0];
        heroItems.GetComponent<HeroItemsController>().reload();
    }

    public void closeInventory() {
        GameMessage gm = new GameMessage();
        gm.type = MessageType.CLOSE_INVENTORY;
        gm.message = "close inventory";
        generateMessage(gm);

        GameMessage gm2 = new GameMessage();
        gm2.type = MessageType.OPEN_MAIN_MENU;
        gm2.message = "open main menu";
        generateMessage(gm2);
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_INVENTORY) {
            gameObject.SetActive(true);
            isFinished = false;
        }
        if (message.type == MessageType.CLOSE_INVENTORY) {
            gameObject.SetActive(false);
            isFinished = true;
        }
    }
}
