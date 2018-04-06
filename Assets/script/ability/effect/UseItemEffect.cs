using script;

public class UseItemEffect : AbstractAbilityEffect {

	public Item item;

    public UseItemEffect() {
        targetsNumber = 1;
    }
		
	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {
        var e = new UseItemEvent();

        e.owner = owner;
        e.target = target;
        e.item = item;
        e.eventTime = startTime;

		EventQueueSingleton.queue.add(e);
    }

    public override void updateLevel(int level) {
    }
}
