using UnityEngine;
using System.Collections;

public class Event {
	public Person owner;
	public Ability ability;
	public float eventTime;
    public float eventDuration = 0.0f;

	public int compareTo(Event o) {
        if (eventTime < o.eventTime) {
            return -1;
        } else {
            return 1;
        }
    }

    public virtual float eventStart() {
        logEvent("Casting ability " + ability.name);
        return owner.eventStart(ability, eventTime);
    }

    public void logEvent(string message) {
        CSVLogger.log(eventTime, owner.name, GetType().ToString(), message);
    }

    public override string ToString() {
        if (ability != null) {
            return "Casting ability " + ability.name;
        }else {
            return "";
        }
    }
}
