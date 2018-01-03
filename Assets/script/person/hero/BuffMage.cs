using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  BuffMage : BaseMage {
	public BuffMage() : base() {
        knownAbilities.Add(new GiantPower());
        knownAbilities.Add(new Weakness());
    }
}
