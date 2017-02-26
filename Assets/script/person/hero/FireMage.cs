using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMage : BaseMage {
	public FireMage(AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy) {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new FireBall(this));

        effectList.Add(new PassiveCritChance(this, 100));
    }
}
