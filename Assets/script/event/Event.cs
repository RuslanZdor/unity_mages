using UnityEngine;
using System.Collections;

public class Event {
	public Person owner;
	public Ability ability;
	public float eventTime;

	public int compareTo(Event o) {
        if (eventTime <= o.eventTime) {
            return -1;
        } else {
            return 1;
        }
    }

    public virtual float eventStart() {
        return owner.eventStart(ability);
    }

    public virtual string toString() {
        if (ability != null) {
            return owner.name + " casting " + ability.name;
        }else {
            return "";
        }
    }
}
