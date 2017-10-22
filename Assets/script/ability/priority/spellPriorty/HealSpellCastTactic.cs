using UnityEngine;
using System.Collections;

public class HealSpellCastTactic : AbstractTactic {
    
	public override int getPriority() {
        bool hasDamage = false;
		foreach (Person p in PartiesSingleton.getParty(person.ally).getLivePersons()) {
            if (p.isDamaged()) {
                hasDamage = true;
            }
        }

        if (person.mana > ability.manaCost
                && hasDamage) {
            return defaultPriority;
        }
        return 0;
    }

    public HealSpellCastTactic(int priority) {
        this.defaultPriority = priority;
    }
}
