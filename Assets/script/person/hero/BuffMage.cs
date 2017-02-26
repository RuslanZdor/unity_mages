using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  BuffMage : BaseMage {
	public BuffMage(AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy) {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new GiantPower(this));
    }
}
