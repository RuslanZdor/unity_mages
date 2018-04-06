using UnityEngine;

public class ShopController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject shopTab;

    public GameObject heroItems;
    public GameObject heroItem;

    public Item activeItem;

    void Start() {
        shopTab = transform.Find("ShopTab").gameObject;
        heroTab = transform.Find(InventoryController.INVENTORY_HERO_TAB).gameObject;
 
        disable();
        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            closeInventory();
        }
    }

    public void reload() {
        shopTab.GetComponent<ShopSideController>().reload();
        heroTab.GetComponent<ShopHeroSideController>().reload();
    }


    public void closeInventory() {
        navigation().saveGame();
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_SHOP) {
            enable();
            reload();
        }
        if (gameObject.activeInHierarchy && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
