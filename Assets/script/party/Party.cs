using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Party {

    public AbilityTargetType ally;
    public AbilityTargetType enemy;

    public Party(AbilityTargetType ally, AbilityTargetType enemy) {
        this.ally = ally;
        this.enemy = enemy;
    }

	private List<GameObject> partyList = new List<GameObject>();

    public List<Person> getLivePersons() {
        List<Person> list = new List<Person>();
        foreach (GameObject go in partyList.FindAll((GameObject person) => person.GetComponent<PersonController>().person.isAlive)) {
            list.Add(go.GetComponent<PersonController>().person);
        }
        return list;
    }

    public bool allDead() {
		if (partyList.FindAll ((GameObject person) => person.GetComponent<PersonController>().person.isAlive).Count > 0) {
			return false;
		}
        return true;
    }

    public List<GameObject> getPartyList() {
        return partyList;
    }

    public void setPartyList(List<GameObject> partyList) {
        this.partyList = partyList;
    }

    public void setEnemy(AbilityTargetType enemy) {
		partyList.ForEach (person => person.GetComponent<PersonController>().person.enemy = enemy);
    }

    public void setAlly(AbilityTargetType ally) {
		partyList.ForEach (person => person.GetComponent<PersonController>().person.ally = ally);
    }

    public void addPerson(GameObject person) {
        person.GetComponent<PersonController>().person.ally = ally;
        person.GetComponent<PersonController>().person.enemy = enemy;
        partyList.Add(person);
        setDefaultPosition();
    }

    public void setDefaultPosition() {
        for (int i = 0; i < partyList.Count; i++) {
            int x = 0;
            if (partyList[i].GetComponent<PersonController>().person.ally == AbilityTargetType.ENEMY) {
                x = 2;
                Vector3 theScale = partyList[i].transform.Find("model").localScale;
                if (theScale.x > 0) {
                    theScale.x *= -1;
                }
                partyList[i].transform.Find("model").localScale = theScale;
            } else {
                x = -2;
            }
            partyList[i].transform.position = new Vector2(x, (i * 2) - 2);
            
        }
    }

    public void removePerson(Person person) {
        partyList.RemoveAll((GameObject go) => go.GetComponent<PersonController>().person.id == person.id);
        setDefaultPosition();
    }
}
