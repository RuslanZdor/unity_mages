using UnityEngine;
using UnityEngine.EventSystems;

public class SkillsController : GameScene, IListenerObject, CanReload {

    private GameObject heroTab;
    private GameObject heroList;

    public GameObject heroImage;

    void Start() {
        heroTab = transform.Find("HeroTab").gameObject;
        heroList = transform.Find("HeroList").gameObject;

        heroList.GetComponent<HeroListController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().person = PartiesSingleton.activeHeroes[0];
        heroTab.GetComponent<HeroTabController>().isSkills = true;
        reload();

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
        generateMessage(new GameMessage(MessageType.CLOSE_SKILLS));
        generateMessage(new GameMessage(MessageType.OPEN_MAIN_MENU));
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_SKILLS) {
            enable();
            isFinished = false;
        }
        if (message.type == MessageType.CLOSE_SKILLS) {
            disable();
            isFinished = true;
        }
    }
}
