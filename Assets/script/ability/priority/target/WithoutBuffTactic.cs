using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WithoutBuffTactic : AbstractTargetTactic {

    public Buff buff;

    public WithoutBuffTactic(Buff buff) {
        this.buff = buff;
    }

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        List<Person> list = party.getLivePersons();
		list.RemoveAll((Person person) => person.hasEffect(buff));

		if (count >= list.Count) {
            return list;
        }else {
            shuffle(list);
			return list.GetRange(0, count);
        }
    }
}
