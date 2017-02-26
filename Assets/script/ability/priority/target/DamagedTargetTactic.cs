using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamagedTargetTactic : AbstractTargetTactic {

	public override List<Person> getTargets(Party party, int count) {
		List<Person> resultList = new List<Person>();
        List<Person> list = party.getLivePersons();
		resultList = list.FindAll ((Person p) => p.isDamaged());

		while (resultList.Count > count) {
            int c = 0;
			for (int i = 0; i < resultList.Count; i++) {
				if (resultList[c].health / resultList[c].maxHealth
					< resultList[i].health / resultList[i].maxHealth) {
                    c = i;
                }
            }
			resultList.Remove(resultList[c]);
        }

        return resultList;
    }
}
