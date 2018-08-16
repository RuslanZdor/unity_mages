using UnityEngine;

public class InventoryController : GameScene, IListenerObject, CanReload {

    public static readonly string INVENTORY_OBJECT = "Inventory";
    public static readonly string INVENTORY_HERO_LIST = "HeroList";
    public static readonly string INVENTORY_HERO_TAB = "HeroTab";
    
    private GameObject heroTab;
    private GameObject heroList;
    private GameObject heroItems;

    void Start() {
        heroTab = transform.Find(INVENTORY_HERO_TAB).gameObject;
        heroList = transform.Find(INVENTORY_HERO_LIST).gameObject;
        heroItems = transform.Find(INVENTORY_OBJECT).gameObject;

        heroTab.GetComponent<HeroTabController>().isItem = true;

        disable();

        registerListener(this);
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().reload();
        heroTab.GetComponent<HeroTabController>().reload();
        heroItems.GetComponent<HeroItemsController>().reload();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_INVENTORY) {
            enable();
            heroList.GetComponent<HeroListController>().person = PartiesSingleton.currentGame.selectedHeroes[0];
            heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.currentGame.selectedHeroes[0];
            heroItems.GetComponent<HeroItemsController>().person = PartiesSingleton.currentGame.selectedHeroes[0];
            reload();
        }

        if (isActive && message.type == MessageType.CLOSE_INVENTORY) {
            navigation().saveGame();
            navigation().closeActiveWindow();
            navigation().openMainMenu();
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
