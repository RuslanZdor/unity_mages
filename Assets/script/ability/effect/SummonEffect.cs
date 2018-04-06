using script;

public class SummonEffect : AbstractAbilityEffect{

	public Person person;
	public float duration;

	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {

		var clonePerson = (Person) person.Clone();
        clonePerson.summoner = owner;
        clonePerson.id = PersonFactory.getNextId();

        var e = new SummonEvent();
        e.owner = owner;
        e.target = target;
        e.person = clonePerson;
        e.eventTime = startTime;
        EventQueueSingleton.queue.add(e);

        if (duration > 0) {
            var removeEvent = new RemoveSummonEvent();
            removeEvent.owner = owner;
            removeEvent.target = target;
            removeEvent.person = clonePerson;
            removeEvent.eventTime = startTime + duration;
            EventQueueSingleton.queue.add(removeEvent);
        }
    }

    public override void updateLevel(int level) {
    }
}
