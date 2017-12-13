using UnityEngine;
using System.Collections;

public class AddBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override float eventStart() {
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
        return 0.0f;
    }
}
