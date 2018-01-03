using UnityEngine;
using System.Collections;

public class ActiveBuff : Buff {

	public ActiveBuff(AbstractTactic tactic) : base(tactic) {
        animationTime = 0.0f;
    }

	public override float eventStart(float startTime) {
		float time = base.eventStart(startTime);
        generateEvents(personOwner, startTime);
        return time;
    }

    public virtual void setPerson(Person person) {
        personOwner = person;
        if (targetType == AbilityTargetType.FRIEND) {
            targetType = person.ally;
        }else {
            targetType = person.enemy;
        }
    }
}