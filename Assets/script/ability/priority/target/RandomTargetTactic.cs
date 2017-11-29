using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomTargetTactic : AbstractTargetTactic{

	public override List<Person> getTargets(Party party, int count, Ability ability) {
        List<Person> list = party.getLivePersons();
        if (isMelee(ability)) {
            if (list.FindAll((Person person) => person.place.row == 1).Count > 0) {
                return getTargets(list.FindAll((Person person) => person.place.row == 1), count);
            }else {
                return getTargets(list.FindAll((Person person) => person.place.row == 2), count);
            }
        } else {
            return getTargets(list, count);
        }
    }

    private List<Person> getTargets(List<Person> list, int count) {
        List<Person> result = new List<Person>();
        if (count >= list.Count) {
            return list;
        } else {
            int agroSum = 0;
            foreach (Person p in list) {
                agroSum += p.agro;
            }
            for (int i = 0; i < count; i++) {
                int r = Random.Range(0, agroSum);
                foreach (Person p in list) {
                    if (r < p.agro) {
                        result.Add(p);
                        agroSum -= p.agro;
                        break;
                    } else {
                        r -= p.agro;
                    }
                }
            }
            return result;
        }

    }
}
