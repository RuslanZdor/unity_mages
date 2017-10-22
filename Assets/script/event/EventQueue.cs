using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventQueue {
	public List<Event> events = new List<Event>();
	public double currentTime;

    public void startEvent(float time) {
        Event e = EventQueueSingleton.queue.events[0];
        if (e.eventTime <= time) {
            EventQueueSingleton.queue.events.RemoveAt(0);
            currentTime = e.eventTime;
            e.eventStart();
        }
    }

    public void add(Event ev) {
        events.Add(ev);
        events.Sort((x, y) => x.compareTo(y));
    }

	public void removePersonEvents(Person person) {
		events.RemoveAll((Event e) => e.owner.id == person.id);
    }
}
