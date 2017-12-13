using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FightStartController : GameScene {

    private GameObject factory;

    public GameScene fightResultTable;

    private GameObject eventLog;

    // Use this for initialization
    void Start() {
        if (!isFinished) {
            background = "texture/fight_scene";
            Sprite image = Resources.Load<Sprite>(background) as Sprite;
            transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;
            eventLog = transform.Find("CurrentEventLog").gameObject;

            factory = transform.Find("GameFactory").gameObject;
            PersonFactory personFactory = factory.GetComponent<PersonFactory>();
            personFactory.setController(this);

            PartiesSingleton.clear();
            EventQueueSingleton.queue.events.Clear();
            EventQueueSingleton.queue.nextEventTime = 0.0f;
            EventQueueSingleton.queue.realTime = Time.fixedTime;
            EventQueueSingleton.queue.fastFight = false;

            foreach (Person p in PartiesSingleton.activeHeroes) {
                PartiesSingleton.heroes.addPerson(personFactory.create(p));
            }

            PartiesSingleton.enemies.addPerson(personFactory.create(new TrollSummoner("troll summoner 1")));
            PartiesSingleton.enemies.addPerson(personFactory.create(new TrollSummoner("troll summoner 2")));

            foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
                hero.generateEvents(0.0f);
            }

            foreach (Person enemy in PartiesSingleton.enemies.getLivePersons()) {
                enemy.generateEvents(0.0f);
            }

            setNextScene(fightResultTable);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isFinished) {
            if (PartiesSingleton.hasWinner()) {
                CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", "Result");
                foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }

                foreach (Person hero in PartiesSingleton.enemies.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }
                isFinished = true;
            } else {
                if (!EventQueueSingleton.queue.fastFight) {
                    EventQueueSingleton.queue.startEvent(Time.fixedTime);
                }
            }
        }
    }

    public void skipFight() {
        Debug.Log("SKIP!");
        EventQueueSingleton.queue.startFastFight();
    }
}
