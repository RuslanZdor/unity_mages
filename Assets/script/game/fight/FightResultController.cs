using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEngine.UI;

public class FightResultController : GameScene, IListenerObject {

    private GameObject result;
    private GameObject resultHeader;

    public GameObject personStatistics;

    void Start() {
        background = "texture/main_scene";
        var image = Resources.Load<Sprite>(background);
        transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;


        result = transform.Find("ResultTable/table").gameObject;
        resultHeader = transform.Find("ResultTable/Header").gameObject;

        registerListener(this);
        disable();
    }

    void Update() {
        base.Update();
        if (isActive) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                closeResults();
                if (PartiesSingleton.isHeroesWinner()) {
                    generateMessage(new GameMessage(MessageType.FIGHT_FINISH_HERO_WINS));
                }
            }
        }
    }

    private void closeResults() {
        navigation().closeActiveWindow();
        navigation().openFightMap();
    }

    private void reload() {
        foreach (Transform child in result.transform) {
            Destroy(child.gameObject);
        }

        if (PartiesSingleton.isHeroesWinner()) {
            resultHeader.GetComponent<Text>().text = "Heroes Wins!";
        } else {
            resultHeader.GetComponent<Text>().text = "Trolls Wins!";
        }

        for (int i = 0; i < PartiesSingleton.heroes.getPartyList().ToArray().Length; i++) {
            var person = PartiesSingleton.heroes.getPartyList().ToArray()[i].GetComponent<PersonController>().person;

            var go = Instantiate(personStatistics, result.transform, true);
            go.transform.Find("name").GetComponent<Text>().text = person.name;
            go.transform.Find("damage").GetComponent<Text>().text = person.statistics.damageDealed.ToString();
            go.transform.Find("heal").GetComponent<Text>().text = person.statistics.heal.ToString();
            go.transform.Find("tank").GetComponent<Text>().text = person.statistics.damageTaken.ToString();

            go.transform.GetComponent<RectTransform>().position = new Vector2(-4, (float)(1.1 - i * 0.5));

        }

        for (int i = 0; i < PartiesSingleton.enemies.getPartyList().ToArray().Length; i++) {
            var person = PartiesSingleton.enemies.getPartyList().ToArray()[i].GetComponent<PersonController>().person;

            var go = Instantiate(personStatistics, result.transform, true);
            go.transform.Find("name").GetComponent<Text>().text = person.name;
            go.transform.Find("damage").GetComponent<Text>().text = person.statistics.damageDealed.ToString();
            go.transform.Find("heal").GetComponent<Text>().text = person.statistics.heal.ToString();
            go.transform.Find("tank").GetComponent<Text>().text = person.statistics.damageTaken.ToString();

            go.transform.GetComponent<RectTransform>().position = new Vector2(1, (float)(1.1 - i * 0.5));

        }

        foreach (var go in PartiesSingleton.heroes.getPartyList()) {
            var person = go.GetComponent<PersonController>().person;
            PartiesSingleton.currentGame.activeHeroes.FindAll(p => p.name.Equals(person.name))
                .ForEach(p => {
                    p.health = person.health;
                    p.mana = person.mana;
                    p.shield = person.shield;
                    p.isAlive = person.isAlive;
                });
        }
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_FIGHT_RESULT) {
            enable();
            reload();
        }
        if (isActive && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
        }
    }
}
