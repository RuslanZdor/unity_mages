using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RowAddShieldAbilityEffect : AddShieldAbilityEffect {

    public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {

        List<Person> targets = PartiesSingleton.getParty(target.ally).getLivePersons().
            FindAll((Person p) => p.place.x == target.place.x);

        foreach (Person p in targets) {
            base.applyEffect(owner, p, startTime, ab);
        }
    }
}
