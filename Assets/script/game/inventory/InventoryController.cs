using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : GameScene, IListenerObject {

    private GameObject heroTab;
    private GameObject heroList;
    private GameObject heroItems;

    public GameObject heroImage;

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
        heroTab.GetComponent<HeroTabController>().isItem = true;
        heroTab.GetComponent<HeroTabController>().reload();

        heroItems.GetComponent<HeroItemsController>().person = PartiesSingleton.activeHeroes[0];
        heroItems.GetComponent<HeroItemsController>().reload();
    }

    public void closeInventory() {
        generateMessage(new GameMessage(MessageType.CLOSE_INVENTORY));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
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
