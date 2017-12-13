using UnityEngine;
using System.Collections;

public class ActiveBuff : Buff {

	public ActiveBuff(Person person, AbstractTactic tactic) : base(person, tactic) {
    }

	public override float eventStart(float startTime) {
		float time = base.eventStart(startTime);
        generateEvents(personOwner, startTime);
        return time;
    }
}