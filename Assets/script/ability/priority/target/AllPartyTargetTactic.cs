using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllPartyTargetTactic : AbstractTargetTactic {

	public override List<Person> getTargets(Party party, int count) {
        List<Person> list = party.getLivePersons();
        return list;
    }
}
