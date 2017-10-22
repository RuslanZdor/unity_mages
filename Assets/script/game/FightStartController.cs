using UnityEngine;
using System.Collections;

public class FightStartController : GameScene {

    public GameObject factory;

    // Use this for initialization
    void Start() {

        PersonFactory personFactory = factory.GetComponent<PersonFactory>();
        PartiesSingleton.heroes.addPerson(personFactory.create<FireMage>("FireMage"));
        PartiesSingleton.heroes.addPerson(personFactory.create<HealMage>("HealMage"));
        PartiesSingleton.heroes.addPerson(personFactory.create<DebuffMage>("DebuffMage"));
        PartiesSingleton.heroes.addPerson(personFactory.create<BuffMage>("BuffMage"));

        PartiesSingleton.enemies.addPerson(personFactory.create<HeavyTroll>("HeavyTroll"));
        PartiesSingleton.enemies.addPerson(personFactory.create<FastTroll>("FastTroll"));
        PartiesSingleton.enemies.addPerson(personFactory.create<TrollSummoner>("TrollSummoner"));
 
        foreach (Person hero in PartiesSingleton.heroes.getLivePersons()) {
            hero.generateEvents();
        }

        foreach (Person enemy in PartiesSingleton.enemies.getLivePersons()) {
            enemy.generateEvents();
        }

    }

    private void FixedUpdate() {
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
                EventQueueSingleton.queue.startEvent(Time.fixedTime);
            }

        }
    }
}
