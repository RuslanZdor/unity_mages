using UnityEngine;
using System.Collections;

public class ActiveBuff : Buff {

	public ActiveBuff() : base() {
        animationTime = 0.0f;
    }

	public override float eventStart(float startTime) {
		float time = base.eventStart(startTime);
        generateEvents(personOwner, startTime);
        return time;
    }
}