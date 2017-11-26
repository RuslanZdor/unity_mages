using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FightResultController : GameScene {

    private GameObject result;
    private GameObject resultHeader;

    public GameObject personStatistics;

    public List<Party> parties = new List<Party>();

    void Start() {
        background = "texture/main_scene";
        Sprite image = Resources.Load<Sprite>(background) as Sprite;
        transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;


        result = transform.Find("ResultTable").gameObject;
        resultHeader = transform.Find("ResultTable/Header").gameObject;

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

            go.transform.GetComponent<RectTransform>().position = new Vector2(-4, ((float)(4 - i * 0.5)));

        }

        for (int i = 0; i < PartiesSingleton.enemies.getPartyList().ToArray().Length; i++) {
            Person person = PartiesSingleton.enemies.getPartyList().ToArray()[i].GetComponent<PersonController>().person;

            GameObject go = Instantiate(personStatistics, result.transform, true);
            go.transform.Find("name").GetComponent<Text>().text = person.name;
            go.transform.Find("damage").GetComponent<Text>().text = person.statistics.damageDealed.ToString();
            go.transform.Find("heal").GetComponent<Text>().text = person.statistics.heal.ToString();
            go.transform.Find("tank").GetComponent<Text>().text = person.statistics.damageTaken.ToString();

            go.transform.GetComponent<RectTransform>().position = new Vector2(1, ((float)(4 - i * 0.5)));

        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isFinished) {
            closeResults();
            isFinished = true;
        }
    }

    private void closeResults() {
        Destroy(result.gameObject);
    }
}
