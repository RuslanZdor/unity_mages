using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class  BuffMage : BaseMage {
	public BuffMage() : base() {
        knownAbilities.Add(XMLFactory.loadAbility("configs/abilities/buff/giantPower"));
        knownAbilities.Add(XMLFactory.loadAbility("configs/abilities/buff/weekness"));
    }
}
