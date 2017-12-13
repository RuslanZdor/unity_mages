using UnityEngine;
using System.Collections;

public class DamageAbilityEffect : AbstractAbilityEffect {

	public override void applyEffect(Person owner, Person target, float startTime) {
        BasicDamageEvent e = new BasicDamageEvent();

		base.value = valueGenerator.getValue();
        Ability ability = new Ability(owner, new MeleeAttackTactic());
		ability.effectList.Add((AbstractAbilityEffect) base.Clone());

        e.owner = owner;
        e.target = target;
        e.ability = ability;
        e.eventTime = startTime;

        EventQueueSingleton.queue.add(e);
    }
}
