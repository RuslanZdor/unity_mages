using UnityEngine;
using UnityEngine.EventSystems;

public class SkillsController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject heroList;

    public GameObject heroImage;

    void Start() {
        heroTab = transform.Find("HeroTab").gameObject;
        heroList = transform.Find("HeroList").gameObject;

        heroTab.GetComponent<HeroTabController>().isSkills = true;

        disable();

        registerListener(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S) && !isFinished) {
            closeSkills();
        }
    }

    public void reload() {
        heroList.GetComponent<HeroListController>().reload();
        heroTab.GetComponent<HeroTabController>().reload();
    }

    public void closeSkills() {
        generateMessage(new GameMessage(MessageType.SAVE_GAME));
        generateMessage(new GameMessage(MessageType.CLOSE_SKILLS));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_SKILLS) {
            enable();

            heroList.GetComponent<HeroListController>().person = PartiesSingleton.selectedHeroes[0];
            heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.selectedHeroes[0];
            reload();
        }
        if (message.type == MessageType.CLOSE_SKILLS) {
            disable();
        }
    }
}
