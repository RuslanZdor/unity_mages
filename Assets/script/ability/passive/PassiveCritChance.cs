using UnityEngine;
using System.Collections;

public class PassiveCritChance : Buff {
	public PassiveCritChance(Person person, double value) : base(person, new DamageSpellCastTactic(3)){
        name ="Passive Crit Change";

        modificator = new CritChanceModificator(value);
    }
}