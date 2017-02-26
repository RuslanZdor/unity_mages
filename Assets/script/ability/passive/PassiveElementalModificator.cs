using UnityEngine;
using System.Collections;

public class PassiveElementalModificator : Buff {
	public PassiveElementalModificator(Person person, EffectAttribures value) : base(person, new DamageSpellCastTactic(3)) {
        name = "Passive Elemental Modificator";

        modificator = new ElementsDamageModificator(value);
    }
}