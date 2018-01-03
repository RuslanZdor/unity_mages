using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMage : BaseMage {
	public FireMage() : base() {
        knownAbilities.Add(new FireWall());
        knownAbilities.Add(new SummonGolem(new SummonCastTactic(1)));
    }

}
