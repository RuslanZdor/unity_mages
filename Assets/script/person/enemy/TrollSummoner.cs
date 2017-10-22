using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrollSummoner : Troll {
	public TrollSummoner() : base() {
        init();
    }

	protected override void init() {
        abilityList.Add(new SummonGolem(this));
        maxMana = 10;
		base.init();
    }

}
