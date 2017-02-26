using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventQueue {
	public List<Event> events = new List<Event>();
	public double currentTime;

    public void startEvent() {
        Event e = EventQueueSingleton.queue.events[0];
		EventQueueSingleton.queue.events.RemoveAt (0);
        currentTime = e.eventTime;
        e.eventStart();
    }

	public void removePersonEvents(Person person) {
		events.RemoveAll((Event e) => e.owner.Equals(person));
    }
}
