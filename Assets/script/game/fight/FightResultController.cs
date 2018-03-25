using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FightResultController : GameScene, IListenerObject {

    private GameObject result;
    private GameObject resultHeader;

    public GameObject personStatistics;

    public List<Party> parties = new List<Party>();

    void Start() {
        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;


        result = transform.Find("ResultTable/table").gameObject;
        resultHeader = transform.Find("ResultTable/Header").gameObject;

        registerListener(this);
        disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            closeResults();
            if (PartiesSingleton.isHeroesWinner()) {
                generateMessage(new GameMessage(MessageType.FIGHT_FINISH_HERO_WINS));
            }
        }
    }

    private void saveHeroes() {

    }

    private void closeResults() {
         generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_RESULT));
         generateMessage(new GameMessage(MessageType.OPEN_FIGHT_MAP));
    }

    private void reload() {
        foreach (Transform child in result.transform) {
            GameObject.Destroy(child.gameObject);
        }

        if (PartiesSingleton.isHeroesWinner()) {
            resultHeader.GetComponent<Text>().text = "Heroes Wins!";
        } else {
            resultHeader.GetComponent<Text>().text = "Trolls Wins!";
        }

        for (int i = 0; i < PartiesSingleton.heroes.getPartyList().ToArray().Length; i++) {
            Person person = PartiesSingleton.heroes.getPartyList().ToArray()[i].GetComponent<PersonController>().person;

            GameObject go = Instantiate(personStatistics, result.transform, true);
            go.transform.Find("name").GetComponent<Text>().text = person.name;
            go.transform.Find("damage").GetComponent<Text>().text = person.statistics.damageDealed.ToString();
            go.transform.Find("heal").GetComponent<Text>().text = person.statistics.heal.ToString();
            go.transform.Find("tank").GetComponent<Text>().text = person.statistics.damageTaken.ToString();

            go.transform.GetComponent<RectTransform>().position = new Vector2(-4, ((float)(1.1 - i * 0.5)));

        }

        for (int i = 0; i < PartiesSingleton.enemies.getPartyList().ToArray().Length; i++) {
            Person person = PartiesSingleton.enemies.getPartyList().ToArray()[i].GetComponent<PersonController>().person;

            GameObject go = Instantiate(personStatistics, result.transform, true);
            go.transform.Find("name").GetComponent<Text>().text = person.name;
            go.transform.Find("damage").GetComponent<Text>().text = person.statistics.damageDealed.ToString();
            go.transform.Find("heal").GetComponent<Text>().text = person.statistics.heal.ToString();
            go.transform.Find("tank").GetComponent<Text>().text = person.statistics.damageTaken.ToString();

            go.transform.GetComponent<RectTransform>().position = new Vector2(1, ((float)(1.1 - i * 0.5)));

        }

        foreach (GameObject go in PartiesSingleton.heroes.getPartyList()) {
            Person person = go.GetComponent<PersonController>().person;
            PartiesSingleton.activeHeroes.FindAll((Person p) => p.name.Equals(person.name))
                .ForEach((Person p) => {
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
        if (message.type == MessageType.CLOSE_FIGHT_RESULT) {
            disable();
        }
    }
}
