using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FightStartController : GameScene, IListenerObject {

    private GameObject factory;

    public GameScene fightResultTable;

    private GameObject eventLog;
    private GameObject enemyPowerCost;
    private GameObject personTable;

    public int fightPower = 50;

    // Use this for initialization
    void Start() {
        if (!isFinished) {
            background = "texture/fight_scene";
            Sprite image = Resources.Load<Sprite>(background) as Sprite;
            transform.Find("background").GetComponent<SpriteRenderer>().sprite = image;
            eventLog = transform.Find("CurrentEventLog").gameObject;
            enemyPowerCost = transform.Find("EnemyPowerCost").gameObject;
            personTable = transform.Find("PersonTable").gameObject;

            factory = transform.Find("GameFactory").gameObject;
            registerListener(this);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isHided) {
            if (PartiesSingleton.hasWinner()) {
                CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", "Result");
                foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }

                foreach (Person hero in PartiesSingleton.enemies.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }
                isFinished = true;

                openFightResult();
            } else {
                if (!EventQueueSingleton.queue.fastFight) {
                    EventQueueSingleton.queue.startEvent(Time.fixedTime);
                }
            }
        }
    }

    public void openFightResult() {
        GameMessage gm = new GameMessage();
        gm.type = MessageType.CLOSE_FIGHT_SCENE;
        gm.message = "close fight scene";
        generateMessage(gm);

        GameMessage gm2 = new GameMessage();
        gm2.type = MessageType.OPEN_FIGHT_RESULT;
        gm2.message = "open fight result";
        generateMessage(gm2);
    }

    private void reload() {
        foreach (Transform child in personTable.transform) {
            GameObject.Destroy(child.gameObject);
        }

        PersonFactory personFactory = factory.GetComponent<PersonFactory>();

        personFactory.availableEnemy.Add(new Golem());
        personFactory.availableEnemy.Add(new HeavyTroll());
        personFactory.availableEnemy.Add(new FastTroll());
        personFactory.availableEnemy.Add(new TrollSummoner());

        personFactory.setController(personTable);

        PartiesSingleton.clear();
        EventQueueSingleton.queue.events.Clear();
        EventQueueSingleton.queue.nextEventTime = 0.0f;
        EventQueueSingleton.queue.realTime = Time.fixedTime;
        EventQueueSingleton.queue.fastFight = false;

        foreach (Person p in PartiesSingleton.activeHeroes) {
            PartiesSingleton.heroes.addPerson(personFactory.create(p));
        }

        int powerCalculated = 0;

        foreach (Person p in personFactory.generatePersonList(fightPower)) {
            powerCalculated += p.calculatePower();
            PartiesSingleton.enemies.addPerson(personFactory.create(p));
        }
        enemyPowerCost.GetComponent<Text>().text = powerCalculated.ToString();


        foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
            hero.generateEvents(0.0f);
        }

        foreach (Person enemy in PartiesSingleton.enemies.getLivePersons()) {
            enemy.generateEvents(0.0f);
        }
    }

    public void skipFight() {
        Debug.Log("SKIP!");
        EventQueueSingleton.queue.startFastFight();
    }

    public void readMessage(GameMessage message) {
        if (message.type == MessageType.OPEN_FIGHT_SCENE) {
            gameObject.SetActive(true);
            fightPower = ((MapPoint)message.parameters[0]).fightPower;
            reload();
            isHided = false;
        }
        if (message.type == MessageType.CLOSE_FIGHT_SCENE) {
            gameObject.SetActive(false);
            isHided = true;
        }
    }
}
