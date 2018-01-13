using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour, IListenerObject {

    public GameScene mainMenu;
    public GameScene inventory;
    public GameScene fightMap;
    public GameScene fightScene;
    public GameScene fightResult;
    public GameScene dialog;
    public GameScene skills;
    public GameScene positions;

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

        f = new BuffMage();
        f.name = "buff mage";
        f.initAbilities();
        PartiesSingleton.activeHeroes.Add(f);

        PartiesSingleton.inventory.Add(XMLFactory.loadItem("configs/items/weapons/troll_mace"));

        GameObject.Find("MessageController").GetComponent<MessageController>()
            .addMessage(new GameMessage(MessageType.CLOSE_MAIN_MENU));

        Instantiate(mainMenu);
        Instantiate(inventory);
        Instantiate(fightMap);
        Instantiate(fightScene);
        Instantiate(fightResult);
        Instantiate(dialog);
        Instantiate(skills);
        Instantiate(positions);
    }

    // Update is called once per frame
    void Update() {

    }

    public void readMessage(GameMessage message) {
    }
}
