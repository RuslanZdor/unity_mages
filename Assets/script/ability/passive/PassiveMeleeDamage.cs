using UnityEngine;
using System.Collections;

public class PassiveMeleeDamage : Buff {

	public PassiveMeleeDamage(float value) : base(new DamageSpellCastTactic(3)) {
        name = "Weapon damage";

        modificator = new IncreaseMeleeDamageModificator(value);
    }
}
