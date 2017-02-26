using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeavyTroll : Troll {
	public HeavyTroll(AbilityTargetType ally, AbilityTargetType enemy) : base(ally, enemy) {
        init();
    }

	protected override void init() {
		base.init();
        effectList.Add(new PassiveBlockChance(this, 50));
        itemList.Add(new Shield(this));
    }

}
