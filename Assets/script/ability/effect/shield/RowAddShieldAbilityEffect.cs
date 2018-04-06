using System.Collections.Generic;
using script;

public class RowAddShieldAbilityEffect : AddShieldAbilityEffect {

    public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {

        var targets = PartiesSingleton.getParty(target.ally).getLivePersons().
            FindAll(p => p.place.x == target.place.x);

        foreach (var p in targets) {
            base.applyEffect(owner, p, startTime, ab);
        }
    }
}
