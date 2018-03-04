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

        heroTab.GetComponent<HeroTabController>().isItem = true;

        disable();

        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            navigation().saveGame();
            navigation().openMainMenu();
            navigation().closeInventory();
        }
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().reload();
        heroTab.GetComponent<HeroTabController>().reload();
        heroItems.GetComponent<HeroItemsController>().reload();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_INVENTORY) {
            enable();

            heroList.GetComponent<HeroListController>().person = PartiesSingleton.selectedHeroes[0];
            heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.selectedHeroes[0];
            heroItems.GetComponent<HeroItemsController>().person = PartiesSingleton.selectedHeroes[0];

            reload();
        }
        if (message.type == MessageType.CLOSE_INVENTORY) {
            disable();
        }
    }
}
