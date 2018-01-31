using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject heroList;
    private GameObject heroItems;

    public GameObject heroImage;

    void Start() {
        heroTab = transform.Find("HeroTab").gameObject;
        heroList = transform.Find("HeroList").gameObject;
        heroItems = transform.Find("Inventory").gameObject;

        heroList.GetComponent<HeroListController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.activeHeroes[0];
        heroItems.GetComponent<HeroItemsController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().isItem = true;
        reload();

        disable();

        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            closeInventory();
        }
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().reload();
        heroTab.GetComponent<HeroTabController>().reload();
        heroItems.GetComponent<HeroItemsController>().reload();
    }

    public void closeInventory() {
        generateMessage(new GameMessage(MessageType.CLOSE_INVENTORY));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_INVENTORY) {
            enable();
        }
        if (message.type == MessageType.CLOSE_INVENTORY) {
            disable();
        }
    }
}
