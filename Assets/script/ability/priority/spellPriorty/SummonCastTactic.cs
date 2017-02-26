using UnityEngine;
using System.Collections;

public class SummonCastTactic : AbstractTactic {

	public Person summon;

	public override int getPriority() {

		long summonCount = PartiesSingleton.getParty(person.ally).partyList.FindAll(
			(Person p) => person.name.Equals(summon.name)
                        && p.Equals(person.summoner)
                        && p.isAlive()
        ).Count;

        if (person.mana > ability.manaCost
                && summonCount == 0) {
            return defaultPriority;
        }
        return 0;
    }

    public SummonCastTactic( int priority) {
        defaultPriority = priority;
    }
}
