using UnityEngine;
using System.Collections;

public class HealAbilityEffect : AbstractAbilityEffect {

	public override void applyEffect(Person owner, Person target, float startTime) {
        BasicTargetEvent e = new BasicHealEvent();

        base.value = valueGenerator.getValue();
        Ability ability = new Ability(owner, new MeleeAttackTactic());
		ability.effectList.Add(this);

		e.ability = ability;
		e.owner = owner;
		e.target = target;
		e.eventTime = startTime;

        EventQueueSingleton.queue.add(e);
    }
}
