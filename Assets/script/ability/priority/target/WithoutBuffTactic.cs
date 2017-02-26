using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WithoutBuffTactic : AbstractTargetTactic {

    public Buff buff;

    public WithoutBuffTactic(Buff buff) {
        this.buff = buff;
    }

	public override List<Person> getTargets(Party party, int count) {
        List<Person> list = party.getLivePersons();
		list.RemoveAll((Person person) => person.hasAttribute(buff));

		if (count >= list.Count) {
            return list;
        }else {
 //           Collections.shuffle(list, new Random());
			return list.GetRange(0, count);
        }
    }
}
