using UnityEngine;
using System.Collections;

public class GenerateAbilityEvent : BasicTargetEvent {
	public override void eventStart() {
        Ability ability = null;
        int priority = -1;
		foreach (Ability ab in owner.abilityList) {
            Party party = PartiesSingleton.getParty(ab.targetType);

			if (ab.abilityTactic.getPriority() > priority
				&& !owner.usedAbilites.Contains(ab)
				&& ab.targetTactic.getTargets(party, 1).Count > 0) {
                ability = ab;
				priority = ab.abilityTactic.getPriority();
            }
        }
        if (ability != null) {
			Debug.Log(EventQueueSingleton.queue.currentTime + " : " + owner.name + " start casting " + ability.name);
            ability.generateEvents(owner);
            owner.usedAbilites.Add(ability);
        }
    }
}
