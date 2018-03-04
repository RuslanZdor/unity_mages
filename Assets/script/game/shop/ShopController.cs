using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject shopTab;

    public GameObject heroItems;
    public GameObject heroItem;

    public Item activeItem;

    void Start() {
        shopTab = transform.Find("ShopTab").gameObject;
        heroTab = transform.Find("HeroTab").gameObject;
 
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
        navigation().closeShop();
        navigation().openMainMenu();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_SHOP) {
            enable();
            reload();
        }
        if (message.type == MessageType.CLOSE_SHOP) {
            disable();
        }
    }
}
