using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllPartyTargetTactic : AbstractTargetTactic {

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        List<Person> list = party.getLivePersons();
        return list;
    }
}
