using UnityEngine;
using System.Collections;

public class BasicHealEvent : BasicTargetEvent {
	public override float eventStart() {
        float value = target.heal(ability);
        owner.statistics.heal += value;

        return 0.0f;
    }

    public override string toString() {
        return owner.name + " make " + " heal to " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]";
    }
}
