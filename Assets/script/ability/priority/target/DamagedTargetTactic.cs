using System.Collections.Generic;
using script;

public class DamagedTargetTactic : AbstractTargetTactic {

	public override List<Person> getTargets(Party party, int count, Ability ability) {
		var resultList = new List<Person>();
        var list = party.getLivePersons();
		resultList = list.FindAll (p => p.isDamaged());

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
