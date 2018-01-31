using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FightStartController : GameScene, IListenerObject {

    private GameObject factory;

    public GameScene fightResultTable;
    public GameObject userAbility;
    public GameObject abilityEvent;

    private GameObject eventLog;
    private GameObject enemyPowerCost;
    private GameObject personTable;
    private GameObject eventLine;

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
            eventLine = transform.Find("EventLine").gameObject;

            factory = transform.Find("GameFactory").gameObject;
            registerListener(this);
            disable();
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
                    string result = EventQueueSingleton.queue.startEvent(Time.fixedTime).ToString();
                    if (result.Length > 0) {
                        eventLog.GetComponent<Text>().text = result;
                        displayEvents(EventQueueSingleton.queue.events);
                    }
                }
            }
        }
    }

    public void openFightResult() {
        generateMessage(new GameMessage(MessageType.CLOSE_FIGHT_SCENE));
        generateMessage(new GameMessage(MessageType.OPEN_FIGHT_RESULT));
    }

    private void reload() {
        foreach (Transform child in personTable.transform) {
            GameObject.Destroy(child.gameObject);
        }

        PersonFactory personFactory = factory.GetComponent<PersonFactory>();

        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/heavyTroll"));
        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/fastTroll"));
        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/trollSummoner"));

        personFactory.setController(personTable);

        PartiesSingleton.clear();
        EventQueueSingleton.queue.events.Clear();
        EventQueueSingleton.queue.nextEventTime = 0.0f;
        EventQueueSingleton.queue.realTime = Time.fixedTime;
        EventQueueSingleton.queue.fastFight = false;

        foreach (Person p in PartiesSingleton.activeHeroes.FindAll((Person p) => p.isActive)) {
            PartiesSingleton.heroes.addPerson(personFactory.create(p));
        }

        int count = 0;
        foreach (Ability ability in PartiesSingleton.player.abilityList) {
            GameObject uAbility = Instantiate(userAbility, transform.Find("UserAbility"), false);
            uAbility.GetComponent<UserAbilityController>().ability = ability;
            uAbility.transform.localPosition = new Vector2(0.0f, 0.0f + 1.2f * count++); ;
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
            enable();
            fightPower = ((MapPoint)message.parameters[0]).fightPower;
            reload();
            isHided = false;
        }
        if (message.type == MessageType.CLOSE_FIGHT_SCENE) {
            disable();
            isHided = true;
        }
    }

    public void displayEvents(List<Event> events) {
        foreach (Transform child in eventLine.transform) {
            Destroy(child.gameObject);
        }

        int count = 0;
        foreach (Event ev in events) {
            if (ev.ability != null
                && ev.ability.name != null
                && ev.owner != null
                && ev.eventDuration > 0) {
                GameObject abEvent = Instantiate(abilityEvent, eventLine.transform, false);
                abEvent.GetComponent<AbilityEventController>().setEvent(ev);
                abEvent.transform.localPosition = new Vector2(0.0f + (1.2f * count++), 0.0f);
            }
        }
    }
}
