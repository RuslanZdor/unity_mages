using UnityEngine;
using System.Collections;

public class SummonEvent : BasicTargetEvent {

	public Person person;

	public override float eventStart() {
        PersonFactory pf = GameObject.Find("GameFactory").GetComponent<PersonFactory>();
        GameObject go = pf.create(person);

        PartiesSingleton.getParty(owner.ally).addPerson(go);
        go.GetComponent<PersonController>().person.summoner = owner;
        go.GetComponent<PersonController>().person.generateEvents();

        owner.statistics.summonCount++;

        return 0.0f; ;
    }

    public override string toString() {
        return owner.name + " summoning " + person.name + "[" + person.health + "/" + person.maxHealth + "]";
    }
}
