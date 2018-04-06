using System.Collections.Generic;
using script;

public class ItselfTargetTactic : AbstractTargetTactic{

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        var result = new List<Person>();
        result.Add(ability.personOwner);
        return result;
    }
}
