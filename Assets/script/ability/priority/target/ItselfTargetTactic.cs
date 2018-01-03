using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItselfTargetTactic : AbstractTargetTactic{

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        List<Person> result = new List<Person>();
        result.Add(ability.personOwner);
        return result;
    }
}
