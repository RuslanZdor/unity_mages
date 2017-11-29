using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventQueue {
	public List<Event> events = new List<Event>();
	public float nextEventTime;

    public string startEvent(float time) {
        if (nextEventTime <= time) {
            Event e = events[0];
            events.RemoveAt(0);
 //           if (e.toString().Length > 0) {
                Debug.Log(nextEventTime + ": " + e.GetType().Name + ":" + e.toString());
 //           }
            nextEventTime += e.eventStart();
            return e.toString();
        }
        return "";
    }

    public void add(Event ev) {
        events.Add(ev);
        events.Sort((x, y) => x.compareTo(y));
    }

	public void removePersonEvents(Person person) {
		events.RemoveAll((Event e) => e.owner.id == person.id);
    }
}
