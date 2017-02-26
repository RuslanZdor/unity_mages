using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItselfTargetTactic : AbstractTargetTactic{

	public List<Person> list;

	public override List<Person> getTargets(Party party, int count) {
        return list;
    }

    public ItselfTargetTactic (Person person) {
		list = new List<Person>();
        list.Add(person);
    }
}
