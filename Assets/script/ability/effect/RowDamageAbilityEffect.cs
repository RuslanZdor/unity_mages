using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowDamageAbilityEffect : AbstractAbilityEffect {

    public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {

        List<Person> targets = PartiesSingleton.getParty(target.ally).getLivePersons().FindAll((Person p) => p.place.row == target.place.row);

        bool isFirst = true;
        foreach (Person p in targets) {
            BasicDamageEvent e = new BasicDamageEvent();

            if (!isFirst) {
                e.eventDuration = 0.0f;
            }else {
                isFirst = false;
            }

            base.value = valueGenerator.getValue();
            Ability ability = new Ability(new MeleeAttackTactic());
            ability.effectList.Add((AbstractAbilityEffect)base.Clone());

            ability.animation = ab.animation;

            e.owner = owner;
            e.target = p;
            e.ability = ability;
            e.eventTime = startTime;

            EventQueueSingleton.queue.add(e);
        }
    }
}
