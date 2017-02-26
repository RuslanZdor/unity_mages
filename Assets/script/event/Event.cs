using UnityEngine;
using System.Collections;

public class Event {
	public Person owner;
	public Ability ability;
	public double eventTime;

	public int compareTo(Event o) {
        if (eventTime < o.eventTime) {
            return -1;
        }else {
            return 1;
        }
    }

    public virtual void eventStart() {
        owner.eventStart(ability);
    }
}
