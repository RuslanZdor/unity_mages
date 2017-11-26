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

        return 0.0f;
    }

    public override string toString() {
        return owner.name + " cast " + buff.name + " to " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]";
    }
}
