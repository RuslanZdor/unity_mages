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
        setDefaultPosition(person);
        partyList.Add(person);
    }

    public void setDefaultPosition(GameObject go) {
        Person person = go.GetComponent<PersonController>().person;
        if (person.place == null) {
            person.place = generatePlace();
        }
        int x = 0;
        if (person.ally == AbilityTargetType.ENEMY) {
            x = 2 * person.place.row;
            Vector3 theScale = go.transform.Find("model").localScale;
            if (theScale.x > 0) {
                theScale.x *= -1;
            }
            go.transform.Find("model").localScale = theScale;
        } else {
           x = -2 * person.place.row;
        }
        go.transform.position = new Vector2(x, (person.place.index * 2) - 4);
    }

    public Place generatePlace() {
        List<Place> list = new List<Place>();
        for (int i = 1; i < 3; i++) {
            for (int j = 1; j < 4; j++) {
                if (isPlaceEmpty(i, j)) {
                    list.Add(new Place(i, j));
                }
            }
        }
        if (list.Count > 0) {
            return list[Random.Range(0, list.Count - 1)];
        }
        return null;
    }

    public bool isPlaceEmpty(int row, int index) {
        foreach (GameObject go in partyList) {
            if (go.GetComponent<PersonController>().person.place.row == row
                && go.GetComponent<PersonController>().person.place.index == index) {
                return false;
            }
        }
        return true;
    }
}
