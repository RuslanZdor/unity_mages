using UnityEngine;
using System.Collections;

public class FightStartController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Person fireMage = new FireMage(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
        fireMage.name = "fire mage";

        Person healMage = new HealMage(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
        healMage.name = "heal mage";

        Person buffMage = new BuffMage(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
        buffMage.name = "buff mage";

        Person debuffMage = new DebuffMage(AbilityTargetType.FRIEND, AbilityTargetType.ENEMY);
        debuffMage.name = "debuff mage";

        PartiesSingleton.heroes.getPartyList().Add(fireMage);
        PartiesSingleton.heroes.getPartyList().Add(healMage);
        PartiesSingleton.heroes.getPartyList().Add(buffMage);
        PartiesSingleton.heroes.getPartyList().Add(debuffMage);

        Person heavyTroll = new HeavyTroll(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);
        heavyTroll.name = "heavy Troll";

        Person fastTroll = new FastTroll(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);
        fastTroll.name = "fast Troll";

        Person trollSummoner = new TrollSummoner(AbilityTargetType.ENEMY, AbilityTargetType.FRIEND);
        trollSummoner.name = "Troll Summoner";

        PartiesSingleton.enemies.getPartyList().Add(heavyTroll);
        PartiesSingleton.enemies.getPartyList().Add(fastTroll);
        PartiesSingleton.enemies.getPartyList().Add(trollSummoner);

        PartiesSingleton.heroes.setEnemy(AbilityTargetType.ENEMY);
        PartiesSingleton.heroes.setAlly(AbilityTargetType.FRIEND);
        PartiesSingleton.enemies.setEnemy(AbilityTargetType.FRIEND);
        PartiesSingleton.enemies.setAlly(AbilityTargetType.ENEMY);

		foreach (Person hero in PartiesSingleton.heroes.getPartyList()) {
            hero.generateEvents();
        }

		foreach (Person enemy in PartiesSingleton.enemies.getPartyList()) {
            enemy.generateEvents();
        }

		while (EventQueueSingleton.queue.events.Count != 0) {
            if (PartiesSingleton.hasWinner()) {
                break;
            }
            EventQueueSingleton.queue.startEvent();
        }

		Debug.Log("Result");
		foreach (Person hero in PartiesSingleton.heroes.getPartyList()) {
			Debug.Log(hero.name + "has " + hero.health);
        }

		foreach (Person hero in PartiesSingleton.enemies.getPartyList()) {
			Debug.Log(hero.name + "has " + hero.health);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
