using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FastTroll : Troll {
	public FastTroll () : base() {
        effectList.Add(new PassiveDodgeChance(this, 20));

        abilityList.Add(new MeleeAttack(this, "Melee Attack 2"));
        numberParrallelCasts = 2;
    }

}
