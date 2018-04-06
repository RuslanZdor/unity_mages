using System.Collections.Generic;
using script;
using UnityEngine;

public class Party {

    public AbilityTargetType ally;
    public AbilityTargetType enemy;

    public Party(AbilityTargetType ally, AbilityTargetType enemy) {
        this.ally = ally;
        this.enemy = enemy;
    }

	private List<GameObject> partyList = new List<GameObject>();

    public List<Person> getLivePersons() {
        var list = new List<Person>();
        foreach (var go in partyList.FindAll(person => person.GetComponent<PersonController>().person.isAlive)) {
            list.Add(go.GetComponent<PersonController>().person);
        }
        return list;
    }

    public bool allDead() {
		if (partyList.FindAll (person => person.GetComponent<PersonController>().person.isAlive).Count > 0) {
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
        var person = go.GetComponent<PersonController>().person;
        if (person.place.x < 1 || person.place.y < 1) {
            person.place = generatePlace();
        }
        float x = 0;
        if (person.ally == AbilityTargetType.ENEMY) {
            x = 2 * person.place.x;
            var theScale = go.transform.Find("model").localScale;
            if (theScale.x > 0) {
                theScale.x *= -1;
            }
            go.transform.Find("model").localScale = theScale;
        } else {
           x = -2 * person.place.x;
        }
        go.transform.position = new Vector2(x, person.place.y * 2 - 4);
    }

    public Vector2 generatePlace() {
        var list = new List<Vector2>();
        for (int i = 1; i < 3; i++) {
            for (int j = 1; j < 4; j++) {
                if (isPlaceEmpty(i, j)) {
                    list.Add(new Vector2(i, j));
                }
            }
        }
        if (list.Count > 0) {
            return list[Random.Range(0, list.Count)];
        }
        return new Vector2();
    }

    public bool isPlaceEmpty(int row, int index) {
        foreach (var go in partyList) {
            if (go.GetComponent<PersonController>().person.place.x == row
                && go.GetComponent<PersonController>().person.place.y == index) {
                return false;
            }
        }
        return true;
    }
}
