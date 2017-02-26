using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FastTroll : Troll {
	public FastTroll (AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy) {
        init();
    }

	protected override void init() {
		base.init();
        effectList.Add(new PassiveDodgeChance(this, 20));

        abilityList.Add(new MeleeAttack(this, "Melee Attack 2"));
        numberParrallelCasts = 2;
    }

}
