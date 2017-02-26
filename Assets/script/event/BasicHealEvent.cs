using UnityEngine;
using System.Collections;

public class BasicHealEvent : BasicTargetEvent {
	public override void eventStart() {
        double value = target.heal(ability);
		Debug.Log(eventTime + " : " + owner.name + " make " + value + " heal to " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]");
    }
}
