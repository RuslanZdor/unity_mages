using UnityEngine;
using System.Collections;

public class SummonEvent : BasicTargetEvent {

	public Person person;

	public override void eventStart() {
        PartiesSingleton.getParty(target.ally).partyList.Add(person);
        person.generateEvents();
		Debug.Log(eventTime + " : " + owner.name + " summoning " + person.name + "[" + person.health + "/" + person.maxHealth + "]");
    }
}
