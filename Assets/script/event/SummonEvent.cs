using UnityEngine;
using System.Collections;

public class SummonEvent : BasicTargetEvent {

	public Person person;

	public override void eventStart() {
        PersonFactory pf = GameObject.Find("GameFactory").GetComponent<PersonFactory>();
        GameObject go = pf.create(person);

        PartiesSingleton.getParty(target.ally).partyList.Add(go);
        person.generateEvents();
		Debug.Log(eventTime + " : " + owner.name + " summoning " + person.name + "[" + person.health + "/" + person.maxHealth + "]");
    }
}
