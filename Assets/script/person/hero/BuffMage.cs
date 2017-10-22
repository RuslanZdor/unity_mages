using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  BuffMage : BaseMage {
	public BuffMage() : base() {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new GiantPower(this));
    }
}
