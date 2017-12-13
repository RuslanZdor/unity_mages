using UnityEngine;
using System.Collections;

public class BasicHealEvent : BasicTargetEvent {
	public override float eventStart() {
        float value = target.heal(ability);
        owner.statistics.heal += value;
        logEvent(" heal " + value + " to "
            + target.name + "[" + target.health + "/" + target.maxHealth + "]");
        return 0.0f;
    }

}
