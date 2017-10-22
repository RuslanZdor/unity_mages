using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealMage : BaseMage {
	public HealMage () : base() {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new HealWave(this));
    }
}
