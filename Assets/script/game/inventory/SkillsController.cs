using UnityEngine;

public class SkillsController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject heroList;

    void Start() {
        heroTab = transform.Find(InventoryController.INVENTORY_HERO_TAB).gameObject;
        heroList = transform.Find(InventoryController.INVENTORY_HERO_LIST).gameObject;

        heroTab.GetComponent<HeroTabController>().isSkills = true;

        disable();

        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            closeSkills();
        }
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().reload();
        heroTab.GetComponent<HeroTabController>().reload();
    }

    public void closeSkills() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
        navigation().closeActiveWindow();
        navigation().openMainMenu();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_SKILLS) {
            enable();

            heroList.GetComponent<HeroListController>().person = PartiesSingleton.selectedHeroes[0];
            heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.selectedHeroes[0];
            reload();
        }
        if (gameObject.activeInHierarchy && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
