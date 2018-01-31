using UnityEngine;
using System.Collections;

public class AddBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override float eventStart() {
        foreach (Buff b in owner.effectList) {
            if (b.modificator != null) {
                b.modificator.owner = owner;
                b.modificator.target = target;
                b.modificator.updateMakingBuff(buff);
            }
        }

        foreach (Buff b in target.effectList) {
            if (b.modificator != null) {
                b.modificator.owner = target;
                b.modificator.target = owner;
                b.modificator.updateGettingBuff(buff);
            }
        }

        target.addEffect(buff);
        if (owner.ally == target.ally) {
            owner.statistics.buffCount++;
        }else {
            owner.statistics.debuffCount++;
        }

        if (owner.enemy.Equals(target.ally)) {
            owner.updateAgro(owner.agro + 1);
        }

        logEvent("add buff " + buff.name + " on " + target.name);

        RemoveBuffEvent removeEvent = new RemoveBuffEvent();
        removeEvent.owner = owner;
        removeEvent.target = target;
        removeEvent.buff = buff;
        removeEvent.eventTime = eventTime + buff.duration;
        EventQueueSingleton.queue.add(removeEvent);

        return 0.0f;
    }
}
