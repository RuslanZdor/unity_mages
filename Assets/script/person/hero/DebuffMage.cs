using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebuffMage : BaseMage {
	public DebuffMage() : base()  {
        init();
    }

	protected override void init() {
		base.init();
        abilityList.Add(new Weakness(this));
    }
}
