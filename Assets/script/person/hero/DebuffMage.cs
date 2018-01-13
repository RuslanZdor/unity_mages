using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebuffMage : BaseMage {
	public DebuffMage() : base()  {
        knownAbilities.Add(XMLFactory.loadAbility("configs/abilities/buff/weekness"));
    }
}
