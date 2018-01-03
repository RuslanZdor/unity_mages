using UnityEngine;
using System.Collections;

public class UseItemEffect : AbstractAbilityEffect {

	public Item item;

    public UseItemEffect() {
        this.targetsNumber = 1;
    }
		
	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {
        UseItemEvent e = new UseItemEvent();

        e.owner = owner;
        e.target = target;
        e.item = item;
        e.eventTime = startTime;

		EventQueueSingleton.queue.add(e);
    }
}
