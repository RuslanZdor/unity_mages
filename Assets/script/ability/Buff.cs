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
    /*
	public override float eventStart(float startTime) {
		foreach (AbstractAbilityEffect effect in effectList) {
            for (int i = 0; i < effect.targetsNumber; i++) {
                Party party = PartiesSingleton.getParty(targetType);
                List<Person> targets = targetTactic.getTargets(party, effect.targetsNumber, this);
				foreach (Person person in targets) {
                    effect.applyEffect(personOwner, person, startTime);
                }
            }
        }
        if (animationTime > 0) {
            personOwner.personController.castAbility();
        }
        return animationTime;
    }
    */
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

	public Buff(AbstractTactic tactic) : base(tactic){
    }
}
