using System.Collections.Generic;
using script;
using script.game;
using UnityEngine;
using UnityEngine.UI;

public class FightStartController : GameScene, IListenerObject {

    private GameObject factory;

    public GameObject userAbility;
    public GameObject abilityEvent;

    private GameObject eventLog;
    private GameObject enemyPowerCost;
    private GameObject personTable;
    private GameObject eventLine;

    public MapPoint mapPoint;

    // Use this for initialization
    void Start() {
        if (!isFinished) {
            background = "texture/fight_scene";
            var image = Resources.Load<Sprite>(background);
            transform.Find(Constants.BACKGROUND).GetComponent<SpriteRenderer>().sprite = image;
            eventLog = transform.Find("CurrentEventLog").gameObject;
            enemyPowerCost = transform.Find("EnemyPowerCost").gameObject;
            personTable = transform.Find("PersonTable").gameObject;
            eventLine = transform.Find("EventLine").gameObject;

            factory = GameObject.Find("GameFactory").gameObject;
            registerListener(this);
            disable();
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isHided) {
            if (PartiesSingleton.hasWinner()) {
                CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", "Result");
                foreach (var hero in PartiesSingleton.heroes.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }

                foreach (var hero in PartiesSingleton.enemies.getLivePersons()) {
                    CSVLogger.log(EventQueueSingleton.queue.nextEventTime, "FightController", "FightController", hero.name + "has " + hero.health);
                }
                isFinished = true;

                openFightResult();
            } else {
                if (!EventQueueSingleton.queue.fastFight) {
                    string result = EventQueueSingleton.queue.startEvent(Time.fixedTime);
                    if (result.Length > 0) {
                        eventLog.GetComponent<Text>().text = result;
                        displayEvents(EventQueueSingleton.queue.events);
                    }
                }
            }
        }
    }

    public void openFightResult() {
        GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().closeActiveWindow();
        GameObject.Find(Constants.NAVIGATION_OBJECT).GetComponent<NavigationController>().openFightResult();
    }

    private void reload() {
        foreach (Transform child in personTable.transform) {
            Destroy(child.gameObject);
        }

        var personFactory = factory.GetComponent<PersonFactory>();

        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/heavyTroll"));
        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/fastTroll"));
        personFactory.availableEnemy.Add(XMLFactory.loadPerson("configs/monsters/trolls/trollSummoner"));

        personFactory.setController(personTable);

        PartiesSingleton.clear();
        EventQueueSingleton.queue.events.Clear();
        EventQueueSingleton.queue.nextEventTime = 0.0f;
        EventQueueSingleton.queue.realTime = Time.fixedTime;
        EventQueueSingleton.queue.fastFight = false;

        foreach (var p in PartiesSingleton.activeHeroes.FindAll(p => p.isActive)) {
            PartiesSingleton.heroes.addPerson(personFactory.create(p));
        }

        int count = 0;
        foreach (var ability in PartiesSingleton.player.abilityList) {
            var uAbility = Instantiate(userAbility, transform.Find("UserAbility"), false);
            uAbility.GetComponent<UserAbilityController>().ability = ability;
            uAbility.transform.localPosition = new Vector2(0.0f, 0.0f + 1.2f * count++); ;
        }

        int powerCalculated = 0;

        foreach (var p in personFactory.generatePersonList(mapPoint)) {
            powerCalculated += p.calculatePower();
            PartiesSingleton.enemies.addPerson(personFactory.create(p));
        }
        foreach (var go in PartiesSingleton.enemies.getPartyList()) {
            go.GetComponent<PersonController>().person.initHealthMana();
        }
        enemyPowerCost.GetComponent<Text>().text = powerCalculated.ToString();


        foreach (var hero in PartiesSingleton.heroes.getLivePersons()) {
            hero.generateEvents(0.0f);
        }

        foreach (var enemy in PartiesSingleton.enemies.getLivePersons()) {
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
            mapPoint = (MapPoint) message.parameters[0];
            reload();
            isHided = false;
        }
        if (gameObject.activeInHierarchy && message.type == MessageType.CLOSE_ACTIVE_WINDOW) {
            disable();
            isHided = true;
        }
    }

    public void displayEvents(List<Event> events) {
        foreach (Transform child in eventLine.transform) {
            Destroy(child.gameObject);
        }

        int count = 0;
        foreach (var ev in events) {
            if (ev.ability != null
                && ev.ability.name != null
                && ev.owner != null
                && ev.eventDuration > 0) {
                var abEvent = Instantiate(abilityEvent, eventLine.transform, false);
                abEvent.GetComponent<AbilityEventController>().setEvent(ev);
                abEvent.transform.localPosition = new Vector2(0.0f + 1.2f * count++, 0.0f);
            }
        }
    }
}
