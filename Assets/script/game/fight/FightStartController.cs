using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightStartController : GameScene {

    private GameObject factory;

    public GameScene fightResultTable;

    private GameObject eventLog;

    // Use this for initialization
    void Start() {
        if (!isFinished) {
            background = "texture/main_scene";
            Sprite image = Resources.Load<Sprite>(background) as Sprite;
            transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;
            eventLog = transform.Find("CurrentEventLog").gameObject;

            factory = transform.Find("GameFactory").gameObject;
            PersonFactory personFactory = factory.GetComponent<PersonFactory>();
            personFactory.setController(this);

            PartiesSingleton.clear();
            EventQueueSingleton.queue.events.Clear();
            EventQueueSingleton.queue.nextEventTime = Time.fixedTime;

            foreach (Person p in PartiesSingleton.activeHeroes) {
                PartiesSingleton.heroes.addPerson(personFactory.create(p));
            }

            PartiesSingleton.enemies.addPerson(personFactory.create(new TrollSummoner()));
            PartiesSingleton.enemies.addPerson(personFactory.create(new TrollSummoner()));

            foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
                hero.generateEvents();
            }

            foreach (Person enemy in PartiesSingleton.enemies.getLivePersons()) {
                enemy.generateEvents();
            }

            setNextScene(fightResultTable);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isFinished) {
            if (PartiesSingleton.hasWinner()) {
                Debug.Log("Result");
                foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
                    Debug.Log(hero.name + "has " + hero.health);
                }

                foreach (Person hero in PartiesSingleton.enemies.getLivePersons()) {
                    Debug.Log(hero.name + "has " + hero.health);
                }
                isFinished = true;
            } else {
                string log = EventQueueSingleton.queue.startEvent(Time.fixedTime);
                if (log.Length > 0) {
                    eventLog.transform.GetComponent<Text>().text = log;
                }
            }
        }
    }
}
