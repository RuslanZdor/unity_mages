using UnityEngine;
using System.Collections;

public class SummonEffect : AbstractAbilityEffect{

	public Person person;
	public double duration;

	public override void applyEffect(Person owner, Person target) {

		Person clonePerson = (Person) person.Clone();
        clonePerson.summoner = owner;
        SummonEvent e = new SummonEvent();
        e.owner = owner;
        e.target = target;
        e.person = clonePerson;
        e.eventTime = EventQueueSingleton.queue.currentTime;
        EventQueueSingleton.queue.events.Add(e);

        RemoveSummonEvent removeEvent = new RemoveSummonEvent();
        removeEvent.owner = owner;
        removeEvent.target = target;
        removeEvent.person = clonePerson;
        removeEvent.eventTime = EventQueueSingleton.queue.currentTime + duration;
        EventQueueSingleton.queue.events.Add(removeEvent);
    }
}
