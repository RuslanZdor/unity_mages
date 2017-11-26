using UnityEngine;
using System.Collections;

public class ActiveBuff : Buff {

	public ActiveBuff(Person person, AbstractTactic tactic) : base(person, tactic) {
    }

	public override float eventStart() {
		float time = base.eventStart();
        generateEvents(personOwner);
        return time;
    }
}