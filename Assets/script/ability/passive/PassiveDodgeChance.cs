using UnityEngine;
using System.Collections;

public class PassiveDodgeChance : Buff {
	public PassiveDodgeChance(Person person, double value) : base(person, new DamageSpellCastTactic(3)) {
        name = "Passive Block Change";

        modificator = new DodgeChanceModificator(value);
    }
}