using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebuffMage : BaseMage {
	public DebuffMage(AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy)  {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new Weakness(this));
    }
}
