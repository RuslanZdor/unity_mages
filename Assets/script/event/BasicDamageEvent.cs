using UnityEngine;
using System.Collections;

public class BasicDamageEvent : BasicTargetEvent {


	public override void eventStart() {

		foreach (Buff buff in owner.effectList) {
            buff.modificator.updateMakingDamage(ability);
        }

		foreach (Buff buff in target.effectList) {
            buff.modificator.updateGettingDamage(ability);
        }

        double value = target.damage(ability);
		Debug.Log(eventTime + " : " + owner.name + " make " + value +
                " damage to " + target.name + "[" + target.health + "/" + target.maxHealth + "]");

        owner.updateAgro(owner.agro + 1);
        target.updateAgro(target.agro - 1);

        if (!target.isAlive()) {
			Debug.Log(eventTime + " : " + target.name + " is dead");
        }
    }

}
