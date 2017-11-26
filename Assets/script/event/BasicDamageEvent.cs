using UnityEngine;
using System.Collections;

public class BasicDamageEvent : BasicTargetEvent {


	public override float eventStart() {

		foreach (Buff buff in owner.effectList) {
            buff.modificator.updateMakingDamage(ability);
        }

		foreach (Buff buff in target.effectList) {
            buff.modificator.updateGettingDamage(ability);
        }

        float value = target.damage(ability);
        owner.statistics.damageDealed += value;
        owner.updateAgro(owner.agro + 1);
        target.updateAgro(target.agro - 1);

        if (value > 0) {
            target.personController.animator.SetTrigger(AnimatorConstants.MODEL_ANIMATOR_ISHITTEN);
        }

        if (!target.isAlive) {
			Debug.Log(eventTime + " : " + target.name + " is dead");
        }

        return 0.0f;
    }

    public override string toString() {
        return owner.name + " make " +
                " damage to " + target.name + "[" + target.health + "/" + target.maxHealth + "]";
    }

}
