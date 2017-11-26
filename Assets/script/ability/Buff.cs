using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Buff : Ability {

	public AbstractModificator modificator;
	public float duration;

	public int priority = 0;

    public Buff clone() {
        Buff cl = null;
        try {
			cl = (Buff) this.MemberwiseClone();
		} catch (Exception e) {
			Debug.LogError(e);
        }

        return cl;
    }

	public override float eventStart() {
		foreach (AbstractAbilityEffect effect in effectList) {
            for (int i = 0; i < effect.targetsNumber; i++) {
                Party party = PartiesSingleton.getParty(targetType);
                List<Person> targets = targetTactic.getTargets(party, effect.targetsNumber);
				foreach (Person person in targets) {
                    effect.applyEffect(personOwner, person);
                }
            }
        }
        return animationTIme;
    }

	public bool Equals(Buff obj) {
        return name.Equals(((Buff) obj).name);
    }

    public int compareTo(Buff o) {
        if (priority < o.priority) {
            return -1;
        }else {
            return 1;
        }
    }

	public Buff(Person person, AbstractTactic tactic) : base(person, tactic){
    }
}
