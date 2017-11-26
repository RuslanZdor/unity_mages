using UnityEngine;
using System.Collections;

public class PassiveMeleeDamage : Buff {

	public PassiveMeleeDamage(Person person, float value) : base(person, new DamageSpellCastTactic(3)) {
        name = "Weapon damage";

        modificator = new IncreaseMeleeDamageModificator(value);
    }
}
