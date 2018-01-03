using UnityEngine;
using System.Collections;

public class CooldownEvent : BasicTargetEvent {
	public override float eventStart() {
        owner.usedAbilites.RemoveAll((Ability ab) => ab.name.Equals(ability.name));
        logEvent(" finish cooldown for ability " + ability.name);
        return 0.0f;
    }
}
