using System.Collections.Generic;
using script;
using UnityEngine;

public class RandomTargetTactic : AbstractTargetTactic{

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        var list = party.getLivePersons();
        if (isMelee(ability))
        {
            if (list.FindAll(person => person.place.x == 1).Count > 0) {
                return getTargets(list.FindAll(person => person.place.x == 1), count);
            }
            return getTargets(list.FindAll(person => person.place.x == 2), count);
        }

	    return getTargets(list, count);
	}

    private List<Person> getTargets(List<Person> list, int count) {
        var result = new List<Person>();
        if (count >= list.Count) {
            return list;
        }

        int agroSum = 0;
        foreach (var p in list) {
            agroSum += p.agro;
        }
        for (int i = 0; i < count; i++) {
            int r = Random.Range(0, agroSum);
            foreach (var p in list)
            {
                if (r < p.agro) {
                    result.Add(p);
                    agroSum -= p.agro;
                    break;
                }

                r -= p.agro;
            }
        }
        return result;

    }
}
