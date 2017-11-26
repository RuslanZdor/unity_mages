using UnityEngine;
using System.Collections;

public class RemoveBuffEvent : BasicTargetEvent{

	public Buff buff;

	public override float eventStart() {
        target.removeEffect(buff);
        return 0.0f;
    }

    public override string toString() {
        return " remove " + buff.name + " from " +
                target.name + "[" + target.health + "/" + target.maxHealth + "]";
    }
}
