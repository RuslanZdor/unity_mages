using UnityEngine;
using System.Collections;

public class AddBuffEffect : AbstractAbilityEffect{

    public Buff buff;

	public override void applyEffect(Person owner, Person target, float startTime, Ability ab) {

        Buff ability = new Buff();
        ability.modificator = buff.modificator;
        ability.name = buff.name;
        ability.duration = buff.duration;
        ability.image = buff.image;
        ability.setAbstractTactic(new MeleeAttackTactic());

        ability.animation = ab.animation;

        AddBuffEvent e = new AddBuffEvent();
        e.owner = owner;
        e.target = target;
        e.buff= ability;
        e.eventTime = startTime;
        EventQueueSingleton.queue.add(e);
    }

    public override void updateLevel(int level) {
    }
}