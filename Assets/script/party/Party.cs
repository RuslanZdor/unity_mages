using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Party {
	public List<Person> partyList = new List<Person>();

    public List<Person> getLivePersons() {
		return partyList.FindAll((Person person) => person.isAlive());
    }

    public bool allDead() {
		if (partyList.FindAll ((Person person) => person.isAlive ()).Count > 0) {
			return false;
		}
        return true;
    }

    public List<Person> getPartyList() {
        return partyList;
    }

    public void setPartyList(List<Person> partyList) {
        this.partyList = partyList;
    }

    public void setEnemy(AbilityTargetType enemy) {
		partyList.ForEach ((Person person) => person.enemy = enemy);
    }

    public void setAlly(AbilityTargetType ally) {
		partyList.ForEach ((Person person) => person.ally = ally);
    }
}
