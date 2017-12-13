using UnityEngine;
using System.Collections;

public class GenerateAbilityEvent : BasicTargetEvent {
	public override float eventStart() {
        Ability ability = null;
        int priority = -1;
		foreach (Ability ab in owner.abilityList) {
            Party party = PartiesSingleton.getParty(ab.targetType);

			if (ab.abilityTactic.getPriority() > priority
				&& !owner.usedAbilites.Contains(ab)
				&& ab.targetTactic.getTargets(party, 1, ab).Count > 0) {
                ability = ab;
				priority = ab.abilityTactic.getPriority();
            }
        }
        if (ability != null) {
            ability.generateEvents(owner, eventTime);
            owner.usedAbilites.Add(ability);
            logEvent("generated next ability " + ability.name);
        }else {
            logEvent("ERROR " + owner.name + " has no available ability to cast");
        }
        return 0.0f;
    }
}
