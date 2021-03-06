using System;
using script;
using UnityEngine;

public class SummonAbility : Ability{

	public Person person;
	public float duration;

    public SummonAbility clone() {
        SummonAbility clone = null;
        try {
			clone = (SummonAbility) Clone();
		} catch (Exception e) {
			Debug.LogError(e);
        }

        return clone;
    }

	public bool Equals(Ability obj) {
        obj = (SummonAbility) obj;
        return name.Equals(((SummonAbility) obj).name);
    }

	public SummonCastTactic getAbilityTactic() {
		return (SummonCastTactic) abilityTactic;
    }

    public void setAbilityTactic(SummonCastTactic abilityTactic) {
		this.abilityTactic = abilityTactic;
    }

    public override void setPerson(Person p) {
        personOwner = p;
        targetType = p.ally;
        person = p;
        abilityTactic.person = person;
    }
}
