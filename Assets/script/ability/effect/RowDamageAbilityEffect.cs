using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowDamageAbilityEffect : AbstractAbilityEffect {

    public override void applyEffect(Person owner, Person target, float startTime) {

        List<Person> targets = PartiesSingleton.getParty(target.ally).getLivePersons().FindAll((Person p) => p.place.row == target.place.row);

        foreach (Person p in targets) {
            BasicDamageEvent e = new BasicDamageEvent();
            e.eventDuration = 0.0f;

            base.value = valueGenerator.getValue();
            Ability ability = new Ability(owner, new MeleeAttackTactic());
            ability.effectList.Add((AbstractAbilityEffect)base.Clone());

            e.owner = owner;
            e.target = p;
            e.ability = ability;
            e.eventTime = startTime;

            EventQueueSingleton.queue.add(e);
        }
    }
}
