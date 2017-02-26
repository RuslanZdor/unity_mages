using UnityEngine;
using System.Collections;

public class ActiveBuff : Buff {

	public ActiveBuff(Person person, AbstractTactic tactic) : base(person, tactic) {
    }

	public override void eventStart() {
		base.eventStart();
		Debug.Log(EventQueueSingleton.queue.currentTime + " : " + personOwner.name + " has passive effect " + name);
        generateEvents(personOwner);
    }
}