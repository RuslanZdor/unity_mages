using UnityEngine;
using System.Collections;

public class DamageAbilityEffect : AbstractAbilityEffect {

	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {
        BasicDamageEvent e = new BasicDamageEvent();

		base.value = valueGenerator.getValue();
        Ability ability = new Ability();
        ability.setAbstractTactic(new MeleeAttackTactic());
		ability.effectList.Add((AbstractAbilityEffect) base.Clone());

        ability.animation = ab.animation;

        e.owner = owner;
        e.target = target;
        e.ability = ability;
        e.eventTime = startTime;

        EventQueueSingleton.queue.add(e);
    }

    public override void updateLevel(int level) {
        valueGenerator.updateLevel(level);
    }
}
