using System.Collections.Generic;
using script;

public class AllPartyTargetTactic : AbstractTargetTactic {

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        var list = party.getLivePersons();
        return list;
    }
}
