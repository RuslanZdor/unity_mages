using UnityEngine;
using System.Collections;

public class PassiveDodgeChance : Buff {
	public PassiveDodgeChance(float value) : base(new DamageSpellCastTactic(3)) {
        name = "Passive Block Change";

        modificator = new DodgeChanceModificator(value);
    }
}