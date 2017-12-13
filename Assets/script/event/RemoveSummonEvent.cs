using UnityEngine;
using System.Collections;

public class RemoveSummonEvent : BasicTargetEvent {

	public Person person;


	public override float eventStart() {
        person.unSummon();
        logEvent("remove summon  " + person.name);
        EventQueueSingleton.queue.removePersonEvents(person);
        return 0.0f;
    }
}
