using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  BuffMage : BaseMage {
	public BuffMage() : base() {
        abilityList.Add(new GiantPower(this));
    }
}
