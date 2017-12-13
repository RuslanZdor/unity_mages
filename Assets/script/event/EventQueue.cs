using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventQueue {
	public List<Event> events = new List<Event>();
	public float nextEventTime;
    public float realTime;

    public bool fastFight = false;

    public float startEvent(float time) {
        if (realTime <= time || fastFight) {
            Event e = events[0];
            events.RemoveAt(0);
            float delta = e.eventStart();
            nextEventTime += delta;
            realTime += delta;
            return nextEventTime;
        }
        return 0;
    }

    public void add(Event ev) {
        int position = 0;
        for (int i = 0; i < events.Count; i++) {
            if (events[i].eventTime < ev.eventTime) {
                position = i;
            }else {
                break;
            }
            position++;
        }

        events.Insert(position, ev);
    }

	public void removePersonEvents(Person person) {
		events.RemoveAll((Event e) => e.owner.id == person.id);
    }

    public void startFastFight() {
        fastFight = true;
        while(!PartiesSingleton.hasWinner() || nextEventTime < 10) {
            EventQueueSingleton.queue.startEvent(nextEventTime);
        }
    }
}
