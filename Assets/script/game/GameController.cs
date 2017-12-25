using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour, IListenerObject {

    private GameScene currentScene;

    public GameScene mainMenu;
    public GameScene inventory;
    public GameScene fightMap;
    public GameScene fightScene;
    public GameScene fightResult;
    public GameScene dialog;

    // Use this for initialization
    void Start() {
        Person f = new FireMage();
        f.name = "Fire mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        f = new HealMage();
        f.name = "Heal mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        f = new FireMage();
        f.name = "Summoner mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        GameMessage gm = new GameMessage();
        gm.type = MessageType.CLOSE_MAIN_MENU;
        gm.message = "open main scene";
        GameObject.Find("MessageController").GetComponent<MessageController>().addMessage(gm);

        Instantiate(mainMenu);
        Instantiate(inventory);
        Instantiate(fightMap);
        Instantiate(fightScene);
        Instantiate(fightResult);
        Instantiate(dialog);
    }

    // Update is called once per frame
    void Update() {

    }

    public void readMessage(GameMessage message) {
    }
}
