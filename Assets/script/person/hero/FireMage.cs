using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMage : BaseMage {
	public FireMage() : base() {
        knownAbilities.Add(new FireWall(this));
        knownAbilities.Add(new SummonGolem(this, new SummonCastTactic(1)));
    }

}
