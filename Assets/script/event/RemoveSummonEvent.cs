using UnityEngine;
using System.Collections;

public class RemoveSummonEvent : BasicTargetEvent {

	public Person person;


	public override void eventStart() {
		Debug.Log(person.name + "was removed");
        PartiesSingleton.getParty(target.ally).partyList.Remove(person);
        EventQueueSingleton.queue.removePersonEvents(person);
    }
}
