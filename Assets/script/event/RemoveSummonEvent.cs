using UnityEngine;
using System.Collections;

public class RemoveSummonEvent : BasicTargetEvent {

	public Person person;


	public override float eventStart() {
        person.unSummon();
//        PartiesSingleton.getParty(target.ally).removePerson(person);
        EventQueueSingleton.queue.removePersonEvents(person);
        return 0.0f;
    }

    public override string toString() {
        return person.name + "was removed";
    }
}
