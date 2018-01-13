using UnityEngine;
using System.Collections;
using System;

public class SummonAbility : Ability{

	public Person person;
	public float duration;

    public SummonAbility clone() {
        SummonAbility clone = null;
        try {
			clone = (SummonAbility) base.Clone();
		} catch (Exception e) {
			Debug.LogError(e);
        }

        return clone;
    }

	public bool Equals(Ability obj) {
        obj = (SummonAbility) obj;
        return name.Equals(((SummonAbility) obj).name);
    }

	public SummonAbility() : base() {
    }

    public SummonCastTactic getAbilityTactic() {
		return (SummonCastTactic) base.abilityTactic;
    }

    public void setAbilityTactic(SummonCastTactic abilityTactic) {
		base.abilityTactic = abilityTactic;
    }

    public override void setPerson(Person p) {
        personOwner = p;
        targetType = p.ally;
        person = p;
        abilityTactic.person = person;
    }
}
