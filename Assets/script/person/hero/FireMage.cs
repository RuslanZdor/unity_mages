using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMage : BaseMage {
	public FireMage() : base() {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new FireBall(this));

        effectList.Add(new PassiveCritChance(this, 100));
    }
}
