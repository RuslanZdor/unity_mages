using UnityEngine;
using System.Collections;

public class SummonCastTactic : AbstractTactic {

	public Person summon;

	public override int getPriority() {

		long summonCount = PartiesSingleton.getParty(person.ally).getLivePersons().FindAll(
			(Person p) => (p.summoner != null && person.name.Equals(p.summoner.name)
                        && p.name.Equals(summon.name)
                        && p.isAlive)
        ).Count;

        if (person.mana >= ability.manaCost
                && summonCount == 0) {
            return defaultPriority;
        }
        return 0;
    }

    public SummonCastTactic( int priority) {
        defaultPriority = priority;
    }
}
