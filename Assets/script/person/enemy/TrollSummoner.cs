using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrollSummoner : Troll {
	public TrollSummoner() : base() {
        knownAbilities.Add(new SummonGolem(this, new SummonCastTactic(3)));
        maxMana = 10;

        name = "troll summoner";
    }

    public TrollSummoner(string name) : base() {
        knownAbilities.Add(new SummonGolem(this, new SummonCastTactic(3)));
        maxMana = 10;

        this.name = name;
    }

}
