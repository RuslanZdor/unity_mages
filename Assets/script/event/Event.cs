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
        if (owner != null) {
            logEvent("Casting ability " + ability.name);
            return owner.eventStart(ability, eventTime);
        } else {
            userLogEvent("Casting ability " + ability.name);
            return ability.eventStart(eventTime);
        }
    }

    public void logEvent(string message) {
        CSVLogger.log(eventTime, owner.name, GetType().ToString(), message);
    }
    public void userLogEvent(string message) {
        CSVLogger.log(eventTime, "user", GetType().ToString(), message);
    }

    public override string ToString() {
        if (ability != null) {
            return "Casting ability " + ability.name;
        }else {
            return "";
        }
    }
}
